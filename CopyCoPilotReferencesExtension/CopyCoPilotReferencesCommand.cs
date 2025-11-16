using CCPRE.Generators.References.Actions;
using CCPRE.Generators.References.Constants;
using CCPRE.Generators.References.Factories;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xyLOGIX.Core.Assemblies.Info;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Files;

namespace CopyCoPilotReferencesExtension
{
    /// <summary>
    /// Defines the publicly-exposed events, methods and properties of a Visual
    /// Studio extension command that copies selected file(s) from Solution
    /// Explorer to the clipboard in GitHub Copilot reference format.
    /// </summary>
    /// <remarks>
    /// This class implements a singleton pattern and registers itself as a menu
    /// command within Visual Studio.
    /// <para />
    /// The command retrieves selected file(s) from Solution Explorer, converts
    /// their path(s) to solution-relative path(s), and formats them as
    /// <c>#file:'relativePath'</c> reference(s) for use with GitHub Copilot.
    /// <para />
    /// If invalid value(s) are encountered during execution (such as
    /// <see
    ///     langword="null" />
    /// DTE service or empty solution), the command will fail
    /// gracefully without copying anything to the clipboard.
    /// </remarks>
    internal sealed class CopyCoPilotReferencesCommand
    {
        /// <summary>
        /// The command identifier value that uniquely identifies this command
        /// within the Visual Studio command system.
        /// </summary>
        /// <remarks>
        /// This value must match the command ID defined in the <c>.vsct</c> file.
        /// </remarks>
        public const int CommandId = 0x0100;

        /// <summary>
        /// The <see cref="T:System.Guid" /> value that identifies the command set
        /// to which this command belongs.
        /// </summary>
        /// <remarks>
        /// This value must match the command set GUID defined in the <c>.vsct</c> file.
        /// </remarks>
        public static readonly Guid CommandSet =
            new Guid("e903358c-f298-4653-a50b-c7853569396f");

        /// <summary>
        /// Reference to an instance of an object that implements the
        /// <see cref="T:System.ComponentModel.Design.IMenuCommandService" /> interface
        /// </summary>
        private readonly IMenuCommandService _commandService;

        /// <summary>
        /// Reference to an instance of
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// that owns this
        /// command.
        /// </summary>
        /// <remarks>
        /// We must use the concrete
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// type here because
        /// the Visual Studio extension infrastructure requires it for proper
        /// package initialization and service retrieval.
        /// </remarks>
        private readonly AsyncPackage _package;

        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any <see langword="static" /> members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static CopyCoPilotReferencesCommand() { }

        /// <summary>
        /// Constructs a new instance of
        /// <see
        ///     cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand" />
        /// and returns a reference to it.
        /// </summary>
        /// <param name="package">
        /// (Required.) Reference to an instance of
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// that owns this
        /// command.
        /// </param>
        /// <param name="commandService">
        /// (Required.) Reference to an instance of
        /// <see
        ///     cref="T:System.ComponentModel.Design.IMenuCommandService" />
        /// that is
        /// used to register this command.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown if either <paramref name="package" /> or
        /// <paramref
        ///     name="commandService" />
        /// is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// This constructor registers the command with Visual Studio's menu system
        /// and attaches the
        /// <see
        ///     cref="M:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.OnBeforeQueryStatus(System.Object,System.EventArgs)" />
        /// event handler to the command's
        /// <see
        ///     cref="E:Microsoft.VisualStudio.Shell.OleMenuCommand.BeforeQueryStatus" />
        /// event.
        /// <para />
        /// If <paramref name="package" /> is <see langword="null" />, an
        /// <see
        ///     cref="T:System.ArgumentNullException" />
        /// is thrown.
        /// <para />
        /// If <paramref name="commandService" /> is <see langword="null" />, an
        /// <see cref="T:System.ArgumentNullException" /> is thrown.
        /// </remarks>
        private CopyCoPilotReferencesCommand(
            [NotLogged] AsyncPackage package,
            [NotLogged] IMenuCommandService commandService
        )
        {
            _package = package ??
                       throw new ArgumentNullException(nameof(package));
            _commandService = commandService ??
                              throw new ArgumentNullException(
                                  nameof(commandService)
                              );

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(Execute, menuCommandID);
            menuItem.BeforeQueryStatus += OnBeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets a reference to an instance of
        /// <see
        ///     cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand" />
        /// that represents the singleton instance of this command.
        /// </summary>
        /// <remarks>
        /// This property is initialized by the
        /// <see
        ///     cref="M:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage)" />
        /// method.
        /// <para />
        /// If accessed before initialization, this property will return
        /// <see
        ///     langword="null" />
        /// .
        /// </remarks>
        public static CopyCoPilotReferencesCommand Instance
        {
            [DebuggerStepThrough] get;
            [DebuggerStepThrough] private set;
        }

        /// <summary>
        /// Ends the specified wait <paramref name="dialog" />, if it is active.
        /// </summary>
        /// <param name="dialog">
        /// (Required.) Reference to an instance of an object that implements the
        /// <see cref="T:Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2" />
        /// interface.
        /// <para />
        /// This parameter can also be set to a <see langword="null" /> reference; in which
        /// case, the method does nothing.
        /// </param>
        /// <remarks>
        /// This method must be called on the UI thread.
        /// <para />
        /// If an exception occurs while ending the dialog, the exception is logged, and
        /// the method continues execution without propagating the exception.
        /// </remarks>
        private static void EndWaitDialog(
            [NotLogged] IVsThreadedWaitDialog2 dialog
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.EndWaitDialog: Checking whether the method parameter, 'dialog', has a null reference for a value..."
                );

                // Check to see if the required parameter, 'dialog', is null. If it is,
                // then write an error message to the log file and then terminate the
                // execution of this method, returning the default return value.
                if (dialog == null)
                {
                    // The method parameter, 'dialog', is required and is not supposed
                    // to have a NULL value.  It does, and this is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.EndWaitDialog: *** ERROR *** A null reference was passed for the method parameter, 'dialog'.  Stopping..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.EndWaitDialog: *** SUCCESS *** We have been passed a valid object reference for the method parameter, 'dialog'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info, "*** FYI *** Ending the wait dialog..."
                );

                dialog.EndWaitDialog(out _);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Handles the command execution: obtains selected items, generates
        /// Copilot reference(s) using the appropriate strategy, and copies them to
        /// the clipboard.
        /// </summary>
        /// <remarks>
        /// This method validates all input(s) and returns eagerly on invalid state.
        /// <para />
        /// All DTE access is kept on the UI thread (STA).
        /// <para />
        /// The method utilizes the
        /// <see
        ///     cref="M:CCPRE.Generators.References.Actions.Determine.TheReferenceGenerationStrategyToUse(System.Object)" />
        /// method to determine the appropriate
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// for each selected item.
        /// <para />
        /// The method then obtains the corresponding
        /// <see
        ///     cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// instance using the
        /// <see
        ///     cref="M:CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.OfType(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)" />
        /// factory method.
        /// <para />
        /// If any generator cannot be obtained or reference generation fails, the
        /// item is skipped and the method continues processing remaining item(s).
        /// </remarks>
        private void Execute([NotLogged] object sender, [NotLogged] EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var operationCompleted = false;
            IVsThreadedWaitDialog2 waitDialog = null;

            try
            {
                var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
                if (dte2 == null)
                    return;

                waitDialog = StartWaitDialog(
                    "Microsoft Visual Studio",
                    "Collecting selection and formatting paths...", "Working..."
                );

                var projectItems = GetSelectedProjectItems(dte2);
                if (projectItems == null)
                    return;
                if (projectItems.Count == 0)
                    return;

                var solutionDir = GetSolutionDirectory(dte2);
                if (string.IsNullOrWhiteSpace(solutionDir))
                    return;

                var formatted = GenerateCopilotReferences(
                    projectItems, solutionDir
                );
                if (formatted == null)
                    return;
                if (formatted.Count == 0)
                    return;

                var text = JoinReferences(formatted);
                if (string.IsNullOrWhiteSpace(text))
                    return;

                TryCopyToClipboard(text);

                operationCompleted = true;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
            finally
            {
                EndWaitDialog(waitDialog);
                _ = operationCompleted;
            }
        }

        /// <summary>
        /// Generates GitHub Copilot reference(s) for the specified
        /// <see cref="T:EnvDTE.ProjectItem" /> collection using the appropriate
        /// strategy for each item.
        /// </summary>
        /// <param name="projectItems">
        /// (Required.) Reference to a read-only list of
        /// <see cref="T:EnvDTE.ProjectItem" /> object(s) for which to generate
        /// reference(s).
        /// </param>
        /// <param name="solutionDirectory">
        /// (Required.) A <see cref="T:System.String" /> that contains the absolute
        /// path to the solution directory.
        /// </param>
        /// <returns>
        /// Reference to a read-only list of <see cref="T:System.String" /> value(s)
        /// containing the formatted GitHub Copilot reference(s), or an empty list if
        /// no reference(s) could be generated.
        /// </returns>
        /// <remarks>
        /// This method iterates through each <see cref="T:EnvDTE.ProjectItem" /> in
        /// <paramref name="projectItems" /> and determines the appropriate
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// using the
        /// <see
        ///     cref="M:CCPRE.Generators.References.Actions.Determine.TheReferenceGenerationStrategyToUse(System.Object)" />
        /// method.
        /// <para />
        /// For each valid generator type, it obtains the corresponding
        /// <see
        ///     cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// instance using the
        /// <see
        ///     cref="M:CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.OfType(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)" />
        /// factory method.
        /// <para />
        /// This method returns an empty list if <paramref name="projectItems" /> is
        /// <see langword="null" />.
        /// <para />
        /// This method returns an empty list if
        /// <paramref name="solutionDirectory" /> is blank or whitespace.
        /// <para />
        /// Item(s) for which no valid generator can be obtained are skipped and not
        /// included in the result.
        /// </remarks>
        [return: NotLogged]
        private static IReadOnlyList<string> GenerateCopilotReferences(
            [NotLogged] IReadOnlyList<ProjectItem> projectItems,
            [NotLogged] string solutionDirectory
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = new List<string>();

            try
            {
                if (projectItems == null)
                    return result;
                if (string.IsNullOrWhiteSpace(solutionDirectory))
                    return result;

                var seen =
                    new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var item in projectItems)
                {
                    if (item == null)
                        continue;

                    var generatorType =
                        Determine.TheReferenceGenerationStrategyToUse(item);

                    if (generatorType == CopilotReferenceGeneratorType.Unknown)
                        continue;

                    var generator =
                        GetCopilotReferenceGenerator.OfType(generatorType);

                    if (generator == null)
                        continue;

                    var reference = generator.Generate(item, solutionDirectory);

                    if (string.IsNullOrWhiteSpace(reference))
                        continue;

                    if (!seen.Add(reference))
                        continue;

                    result.Add(reference);
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = new List<string>();
            }

            return result;
        }

        /// <summary>
        /// Gets the currently selected <see cref="T:EnvDTE.ProjectItem" /> entries from
        /// Solution Explorer.
        /// Falls back to the active document’s <see cref="T:EnvDTE.ProjectItem" />.
        /// </summary>
        /// <param name="dte2">Reference to an instance of <see cref="T:EnvDTE80.DTE2" />.</param>
        /// <returns>
        /// A read-only list of selected <see cref="T:EnvDTE.ProjectItem" />
        /// objects.
        /// </returns>
        /// <remarks>
        /// Eagerly returns an empty list when selection is unavailable. Does not walk the
        /// UI hierarchy tree.
        /// </remarks>
        [return: NotLogged]
        private static IReadOnlyList<ProjectItem> GetSelectedProjectItems(
            [NotLogged] DTE2 dte2
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = new AdvisableCollection<ProjectItem>();

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSelectedProjectItems: Checking whether the variable, 'dte2', has a null reference for a value..."
                );

                // Check to see if the variable, dte2, is null.  If it is, send an error
                // to the log file and terminate the execution of this method, returning
                // the default return value.
                if (dte2 == null)
                {
                    // the variable dte2 is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** ERROR ***  The variable, 'dte2', has a null reference.  Stopping..."
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, dte2, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** SUCCESS *** The variable, 'dte2', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to get selected items from Solution Explorer..."
                );

                var selected = dte2.SelectedItems;

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSelectedProjectItems: Checking whether the variable, 'selected', has a null reference for a value..."
                );

                // Check to see if the variable, selected, is null.  If it is, send an error
                // to the log file, and then terminate the execution of this method,
                // returning the default return value.
                if (selected == null)
                {
                    // the variable selected is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** ERROR ***  The variable, 'selected', has a null reference.  Stopping..."
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, selected, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** SUCCESS *** The variable, 'selected', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Going over the selected items..."
                );

                foreach (SelectedItem sel in selected)
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "CopyCoPilotReferencesCommand.GetSelectedProjectItems: Checking whether the variable 'sel' has a null reference for a value..."
                    );

                    // Check to see if the variable, sel, is null. If it is, send an error to the log file and continue to the next loop iteration.
                    if (sel == null)
                    {
                        // the variable sel is required to have a valid object reference.
                        DebugUtils.WriteLine(
                            DebugLevel.Error,
                            "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** ERROR ***  The 'sel' variable has a null reference.  Skipping to the next loop iteration..."
                        );

                        // continue to the next loop iteration.
                        continue;
                    }

                    // We can use the variable, sel, because it's not set to a null reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "CopyCoPilotReferencesCommand.GetSelectedProjectItems: *** SUCCESS *** The 'sel' variable has a valid object reference for its value.  Proceeding..."
                    );

                    // Pattern matching + eager-continue
                    if (sel.ProjectItem is ProjectItem pi)
                    {
                        result.Add(pi);
                        continue;
                    }

                    // If a project (not a file) is selected, skip; this command targets files.
                    if (sel.Project != null) continue;
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = new AdvisableCollection<ProjectItem>();
            }

            return result;
        }

        /// <summary>
        /// Computes the solution directory from <see cref="T:EnvDTE80.DTE2" />.
        /// </summary>
        /// <param name="dte2">Reference to an instance of <see cref="T:EnvDTE80.DTE2" />.</param>
        /// <returns>Absolute path to the solution directory, or <see langword="null" />.</returns>
        [return: NotLogged]
        private static string GetSolutionDirectory([NotLogged] DTE2 dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = string.Empty;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSolutionDirectory: Checking whether the variable, 'dte2', has a null reference for a value..."
                );

                // Check to see if the variable, dte2, is null.  If it is, send an error
                // to the log file and terminate the execution of this method, returning
                // the default return value.
                if (dte2 == null)
                {
                    // the variable dte2 is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.GetSolutionDirectory: *** ERROR ***  The variable, 'dte2', has a null reference.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopyCoPilotReferencesCommand.GetSolutionDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, dte2, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSolutionDirectory: *** SUCCESS *** The variable, 'dte2', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSolutionDirectory: Checking whether the property, 'dte2.Solution', has a null reference for a value..."
                );

                // Check to see if the required property, 'dte2.Solution', has a null reference for a value. 
                // If that is the case, then we will write an error message to the log file, and then
                // terminate the execution of this method, while returning the default return value.
                if (dte2.Solution == null)
                {
                    // The property, 'dte2.Solution', has a null reference for a value.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.GetSolutionDirectory: *** ERROR *** The property, 'dte2.Solution', has a null reference for a value.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopyCoPilotReferencesCommand.GetSolutionDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSolutionDirectory: *** SUCCESS *** The property, 'dte2.Solution', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to get the fully-qualified pathname of the solution..."
                );

                var slnPath = dte2.Solution.FullName;

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.GetSolutionDirectory: Checking whether the variable, 'slnPath', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'slnPath', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(slnPath))
                {
                    // the variable slnPath is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.GetSolutionDirectory: *** ERROR *** The variable, 'slnPath', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"CopyCoPilotReferencesCommand.GetSolutionDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopyCoPilotReferencesCommand.GetSolutionDirectory: *** SUCCESS *** {slnPath.Length} B of data appear to be present in the variable, 'slnPath'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to get the directory name from the solution path..."
                );

                var dir = Path.GetDirectoryName(slnPath);

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopyCoPilotReferencesCommand.GetSolutionDirectory *** INFO: Checking whether the folder with path, '{dir}', exists on the file system..."
                );

                // Check whether a folder having the path, 'dir', exists on the file system.
                // If it does not, then write an error message to the log file, and then terminate
                // the execution of this method, returning the default return value.
                if (!Does.FolderExist(dir))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"CopyCoPilotReferencesCommand.GetSolutionDirectory: *** ERROR *** The system could not locate the folder having the path, '{dir}', on the file system.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopyCoPilotReferencesCommand.GetSolutionDirectory: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopyCoPilotReferencesCommand.GetSolutionDirectory: *** SUCCESS *** The folder with path, '{dir}', was found on the file system.  Proceeding..."
                );

                result = dir;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"CopyCoPilotReferencesCommand.GetSolutionDirectory: Result = '{result}'"
            );

            return result;
        }

        /// <summary>
        /// Initializes the singleton instance of this command asynchronously.
        /// </summary>
        /// <param name="package">
        /// (Required.) Reference to an instance of
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// that owns this
        /// command.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the
        /// asynchronous initialization operation.
        /// </returns>
        /// <remarks>
        /// This method switches to the main UI thread, retrieves the menu command
        /// service, and constructs the singleton instance of
        /// <see
        ///     cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand" />
        /// .
        /// <para />
        /// If <paramref name="package" /> is <see langword="null" />, the method
        /// will fail and the
        /// <see
        ///     cref="P:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Instance" />
        /// property will remain <see langword="null" />.
        /// <para />
        /// If the menu command service cannot be retrieved, initialization may fail
        /// and the
        /// <see
        ///     cref="P:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Instance" />
        /// property will remain <see langword="null" />.
        /// </remarks>
        public static async Task InitializeAsync(
            [NotLogged] AsyncPackage package
        )
        {
            try
            {
                Debug.WriteLine(
                    "CopyCoPilotReferencesCommand.InitializeAsync: *** FYI *** Attempting to initialize the logging subsystem..."
                );

                ProgramFlowHelper.StartDebugger();

                LoggingSubsystemManager.InitializeLogging(
                    muteConsole: false,
                    infrastructureType: LoggingInfrastructureType.PostSharp,
                    logFileName: Get.LogFilePath(),
                    applicationName: Get.ApplicationProductName()
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.InitializeAsync: Checking whether the method parameter, 'package', has a null reference for a value..."
                );

                // Check to see if the required parameter, 'package', is null. If it is,
                // then write an error message to the log file and then terminate the
                // execution of this method, returning the default return value.
                if (package == null)
                {
                    // The method parameter, 'package', is required and is not supposed
                    // to have a NULL value.  It does, and this is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.InitializeAsync: *** ERROR *** A null reference was passed for the method parameter, 'package'.  Stopping..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.InitializeAsync: *** SUCCESS *** We have been passed a valid object reference for the method parameter, 'package'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Switching to the main UI thread..."
                );

                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(
                    package.DisposalToken
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** CopyCoPilotReferencesCommand.InitializeAsync: Checking whether the menu command service could be obtained..."
                );

                // Check to see whether the menu command service could be obtained.
                // If this is not the case, then write an FYI message to the log file
                // explaining that there is nothing more that can be done, and then 
                // terminate the execution of this method.
                if (!(await package.GetServiceAsync(typeof(IMenuCommandService))
                        is IMenuCommandService commandService))
                {
                    // The menu command service was NOT obtained.  There is nothing to do.
                    DebugUtils.WriteLine(
                        DebugLevel.Info,
                        "*** FYI *** The menu command service was NOT obtained.  Nothing to do..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.InitializeAsync: *** SUCCESS *** The menu command service could be obtained.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Setting the value of the 'Instance' property..."
                );

                Instance = new CopyCoPilotReferencesCommand(
                    package, commandService
                );
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Joins tokens into a single space-separated string.
        /// </summary>
        /// <param name="tokens">Reference to a list of tokens.</param>
        /// <returns>A single string suitable for the clipboard.</returns>
        [return: NotLogged]
        private static string JoinReferences(
            [NotLogged] IReadOnlyList<string> tokens
        )
        {
            var result = default(string);
            try
            {
                if (tokens == null) return result;
                if (tokens.Count == 0) return result;

                // Avoid LINQ Join to keep style consistent
                var capacity = 0;
                for (var i = 0; i < tokens.Count; i++)
                {
                    var t = tokens[i];
                    if (string.IsNullOrWhiteSpace(t)) continue;
                    capacity += t.Length + 1;
                }

                var builder = new StringBuilder(capacity);
                var first = true;
                for (var i = 0; i < tokens.Count; i++)
                {
                    var t = tokens[i];
                    if (string.IsNullOrWhiteSpace(t)) continue;

                    if (!first) builder.Append(' ');
                    builder.Append(t);
                    first = false;
                }

                result = builder.ToString();
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = default;
            }

            return result;
        }

        /// <summary>
        /// Normalizes path separators to forward slashes for Copilot Chat.
        /// </summary>
        [return: NotLogged]
        private static string NormalizeSeparators(
            [NotLogged] string relativePath
        )
        {
            var result = default(string);
            try
            {
                if (string.IsNullOrWhiteSpace(relativePath)) return result;

                result = relativePath.Replace('\\', '/');
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = default;
            }

            return result;
        }

        /// <summary>
        /// Handles the
        /// <see
        ///     cref="E:Microsoft.VisualStudio.Shell.OleMenuCommand.BeforeQueryStatus" />
        /// event to control the visibility and enabled state of the command.
        /// </summary>
        /// <param name="sender">
        /// (Required.) Reference to an instance of <see cref="T:System.Object" />
        /// that represents the source of the event, expected to be an instance of
        /// <see cref="T:Microsoft.VisualStudio.Shell.OleMenuCommand" />.
        /// </param>
        /// <param name="e">
        /// (Required.) Reference to an instance of <see cref="T:System.EventArgs" /> that
        /// contains the event data.
        /// </param>
        /// <remarks>
        /// This method ensures the command is visible and enabled whenever it is
        /// queried.
        /// <para />
        /// If <paramref name="sender" /> is not an instance of
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.OleMenuCommand" />
        /// , no action is
        /// taken.
        /// </remarks>
        private void OnBeforeQueryStatus(
            [NotLogged] object sender,
            [NotLogged] EventArgs e
        )
        {
            try
            {
                if (!(sender is OleMenuCommand myCommand))
                    return;

                myCommand.Visible = true;
                myCommand.Enabled = true;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Creates and starts a <c>Threaded Wait Dialog</c> with the specified
        /// <paramref name="caption" />, <paramref name="message" />, and
        /// <paramref name="status" /> text.
        /// </summary>
        /// <remarks>
        /// This method initializes and displays a <c>Threaded Wait Dialog</c>
        /// using Visual Studio's <c>Wait Dialog Service</c> component..
        /// <para />
        /// If the service is unavailable or an error occurs during initialization, the
        /// method returns <see langword="null" />.
        /// </remarks>
        /// <param name="caption">
        /// (Required.) A <see cref="T:System.String" /> containing
        /// the caption text to display in the titlebar of the <c>Wait Dialog</c>.
        /// </param>
        /// <param name="message">
        /// (Required.) A <see cref="T:System.String" /> containing
        /// the main message text to display in the <c>Wait Dialog</c>.
        /// </param>
        /// <param name="status">
        /// (Required.) A <see cref="T:System.String" /> containing
        /// the status text to display in the <c>Wait Dialog</c>, providing additional
        /// context or progress information.
        /// </param>
        /// <returns>
        /// Reference to an instance of an object that implements the
        /// <see cref="T:Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2" />
        /// interface representing the active <c>Wait Dialog</c>, or
        /// <see langword="null" /> if the dialog could not be created.
        /// </returns>
        [return: NotLogged]
        private static IVsThreadedWaitDialog2 StartWaitDialog(
            [NotLogged] string caption,
            [NotLogged] string message,
            [NotLogged] string status
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsThreadedWaitDialog2 result = default;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** CopyCoPilotReferencesCommand.StartWaitDialog: Checking whether the <c>Threaded Wait Dialog</c> service could be activated..."
                );

                // Check to see whether the <c>Threaded Wait Dialog</c> service could be activated.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!(Package.GetGlobalService(
                        typeof(SVsThreadedWaitDialogFactory)
                    ) is IVsThreadedWaitDialogFactory factory))
                {
                    // The <c>Threaded Wait Dialog</c> service could NOT be activated.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The <c>Threaded Wait Dialog</c> service could NOT be activated.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopyCoPilotReferencesCommand.StartWaitDialog: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.StartWaitDialog: *** SUCCESS *** The <c>Threaded Wait Dialog</c> service was activated.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Creating the <c>Threaded Wait Dialog</c>..."
                );

                factory.CreateInstance(out result);

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.StartWaitDialog: Checking whether the variable, 'result', has a null reference for a value..."
                );

                // Check to see if the variable, result, is null.  If it is, send an error
                // to the log file, and then terminate the execution of this method,
                // returning the default return value.
                if (result == null)
                {
                    // the variable result is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.StartWaitDialog: *** ERROR ***  The variable, 'result', has a null reference.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopyCoPilotReferencesCommand.StartWaitDialog: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, result, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.StartWaitDialog: *** SUCCESS *** The variable, 'result', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Requesting that Visual Studio display the <c>Threaded Wait Dialog</c> with caption, '{caption}'; message, '{message}'; and status, '{status}'..."
                );

                // Cancel disabled; marquee enabled
                result.StartWaitDialog(
                    caption, message, status, null, null, 0, false, true
                );
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = default;
            }

            DebugUtils.WriteLine(
                result != null ? DebugLevel.Info : DebugLevel.Error,
                result != null
                    ? "*** SUCCESS *** Obtained a reference to the <c>Threaded Wait Dialog</c>.  Proceeding..."
                    : "*** ERROR *** FAILED to obtain a reference to the <c>Threaded Wait Dialog</c>.  Stopping..."
            );

            return result;
        }

        /// <summary>
        /// Makes an attempt to copy the specified <paramref name="text" /> to the
        /// Clipboard.
        /// </summary>
        /// <param name="text">
        /// (Required.) A <see cref="T:System.String" /> containing the text that is to be
        /// copied to the Clipboard.
        /// </param>
        /// <remarks>
        /// If the specified <paramref name="text" /> is <see langword="null" />,
        /// blank, or the <see cref="F:System.String.Empty" /> value, then the method does
        /// nothing.
        /// </remarks>
        private static void TryCopyToClipboard([NotLogged] string text)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopyCoPilotReferencesCommand.TryCopyToClipboard: Checking whether the variable, 'text', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'text', is null or blank. If it is, 
                // then send an  error to the log file and then terminate the execution of this
                // method.
                if (string.IsNullOrWhiteSpace(text))
                {
                    // the variable text is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopyCoPilotReferencesCommand.TryCopyToClipboard: *** ERROR *** The variable, 'text', has a null reference or is blank.  Stopping..."
                    );

                    // stop.
                    return;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopyCoPilotReferencesCommand.TryCopyToClipboard: *** SUCCESS *** {text.Length} B of data appear to be present in the variable, 'text'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Copying {text.Length} B of the text to the Clipboard..."
                );

                Clipboard.SetText(text);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Exposes static methods to obtain data from various data sources.
        /// </summary>
        private static class Get
        {
            /// <summary>
            /// A <see cref="T:System.String" /> containing the final piece of the path of the
            /// log file.
            /// </summary>
            private static readonly string LOG_FILE_PATH_TERMINATOR =
                $@"{AssemblyCompany}\{AssemblyProduct}\Logs\{AssemblyTitle}_log.txt";

            /// <summary>
            /// Gets a <see cref="T:System.String" /> that contains the product name defined
            /// for this application.
            /// </summary>
            /// <remarks>
            /// This property is really an alias for the
            /// <see cref="P:xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.AssemblyCompany" />
            /// property.
            /// </remarks>
            private static string AssemblyCompany
                => AssemblyMetadata.AssemblyCompany;

            /// <summary>
            /// Gets a <see cref="T:System.String" /> that contains the product name defined
            /// for this application.
            /// </summary>
            /// <remarks>
            /// This property is really an alias for the
            /// <see cref="P:xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.ShortProductName" />
            /// property.
            /// </remarks>
            private static string AssemblyProduct
                => AssemblyMetadata.ShortProductName;

            /// <summary>
            /// Gets a <see cref="T:System.String" /> that contains the assembly title defined
            /// for this application.
            /// </summary>
            /// <remarks>
            /// This property is really an alias for the
            /// <see cref="P:xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.AssemblyTitle" />
            /// property --- except that all whitespace is replace with underscores.
            /// </remarks>
            private static string AssemblyTitle
                => AssemblyMetadata.AssemblyTitle.Replace(" ", "_");

            /// <summary>
            /// Gets a <see cref="T:System.String" /> that contains a user-friendly name for
            /// the software product of which this application or class library is a part.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String" /> that contains a user-friendly name for the
            /// software product of which this application or class library is a part.
            /// </returns>
            public static string ApplicationProductName()
            {
                string result;

                try
                {
                    result = AssemblyProduct;
                }
                catch (Exception ex)
                {
                    // dump all the exception info to the log
                    DebugUtils.LogException(ex);

                    result = string.Empty;
                }

                return result;
            }

            /// <summary>
            /// Obtains a <see cref="T:System.String" /> that contains the fully-qualified
            /// pathname of the file that should be used for logging messages.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String" /> that contains the fully-qualified pathname of
            /// the file that should be used for logging messages.
            /// </returns>
            public static string LogFilePath()
                => Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData
                    ), LOG_FILE_PATH_TERMINATOR
                );
        }
    }
}