using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using PostSharp.Patterns.Diagnostics;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using xyLOGIX.Core.Debug;

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
        /// <b>NOTE:</b> The purpose of this field is to cache the value of the
        /// <see
        ///     cref="P:CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Package" />
        /// property.
        /// <para />
        /// We must use the concrete
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// type here because
        /// the Visual Studio extension infrastructure requires it for proper
        /// package initialization and service retrieval.
        /// </remarks>
        private readonly AsyncPackage _package;

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
        /// Gets a reference to an instance of
        /// <see
        ///     cref="T:System.IServiceProvider" />
        /// that provides access to Visual
        /// Studio service(s).
        /// </summary>
        /// <remarks>
        /// This property returns the underlying
        /// <see
        ///     cref="T:Microsoft.VisualStudio.Shell.AsyncPackage" />
        /// as an
        /// <see
        ///     cref="T:System.IServiceProvider" />
        /// to follow interface-based design
        /// principles.
        /// </remarks>
        private IServiceProvider ServiceProvider
        {
            [DebuggerStepThrough] get => _package;
        }

        /// <summary>
        /// Executes the command logic to copy selected file reference(s) to the
        /// clipboard.
        /// </summary>
        /// <param name="sender">
        /// (Required.) Reference to an instance of <see cref="T:System.Object" />
        /// that represents the source of the event.
        /// </param>
        /// <param name="e">
        /// (Required.) Reference to an instance of <see cref="T:System.EventArgs" /> that
        /// contains the event data.
        /// </param>
        /// <remarks>
        /// This method retrieves selected file(s) from Solution Explorer, computes
        /// their path(s) relative to the solution directory, formats them as
        /// <c>#file:'relativePath'</c> reference(s), and copies the formatted
        /// reference(s) to the clipboard.
        /// <para />
        /// The relative path(s) use forward slash(es) (<c>/</c>) as separator(s)
        /// for compatibility with GitHub Copilot's reference format.
        /// <para />
        /// If no valid file(s) are selected, the method returns early without
        /// modifying the clipboard.
        /// <para />
        /// If the DTE service is unavailable, the method returns early without
        /// modifying the clipboard.
        /// <para />
        /// If the solution is not loaded, the method returns early without
        /// modifying the clipboard.
        /// <para />
        /// This method must be called on the UI thread; otherwise, an exception
        /// will be thrown by
        /// <see
        ///     cref="M:Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread(System.String)" />
        /// .
        /// </remarks>
        private void Execute([NotLogged] object sender, [NotLogged] EventArgs e)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                var dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
                if (dte == null)
                    return;

                if (dte.Solution == null)
                    return;

                if (string.IsNullOrWhiteSpace(dte.Solution.FullName))
                    return;

                var uih = dte.ToolWindows.SolutionExplorer;
                if (uih == null)
                    return;

                if (!(uih.SelectedItems is Array selectedItems))
                    return;

                if (selectedItems.Length <= 0)
                    return;

                var solutionDir = Path.GetDirectoryName(dte.Solution.FullName);
                if (string.IsNullOrWhiteSpace(solutionDir))
                    return;

                var filePaths = selectedItems.Cast<UIHierarchyItem>()
                                             .Select(item
                                                 => item.Object as ProjectItem
                                             )
                                             .Where(projItem => projItem != null
                                             )
                                             .Where(projItem
                                                 => projItem.FileCount > 0
                                             )
                                             .Select(projItem
                                                 => GetCopilotReference(
                                                     projItem, solutionDir
                                                 )
                                             )
                                             .Where(reference
                                                 => !string.IsNullOrWhiteSpace(
                                                     reference
                                                 )
                                             )
                                             .ToList();

                if (!filePaths.Any())
                    return;

                Clipboard.SetText(string.Join(" ", filePaths));
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Gets a GitHub Copilot reference string for the specified
        /// <paramref
        ///     name="projectItem" />
        /// .
        /// </summary>
        /// <param name="projectItem">
        /// (Required.) Reference to an instance of
        /// <see
        ///     cref="T:EnvDTE.ProjectItem" />
        /// for which to generate the reference.
        /// </param>
        /// <param name="solutionDir">
        /// (Required.) A <see cref="T:System.String" /> that contains the solution
        /// directory path.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.String" /> containing the GitHub Copilot
        /// reference in the format <c>#file:'relativePath'</c>, or
        /// <see
        ///     cref="F:System.String.Empty" />
        /// if the reference cannot be generated.
        /// </returns>
        /// <remarks>
        /// This method computes the relative path from the solution directory to
        /// the project item's file and formats it as a GitHub Copilot reference.
        /// <para />
        /// If <paramref name="projectItem" /> is <see langword="null" />, the
        /// method returns <see cref="F:System.String.Empty" />.
        /// <para />
        /// If <paramref name="solutionDir" /> is blank or whitespace, the method
        /// returns <see cref="F:System.String.Empty" />.
        /// </remarks>
        [return: NotLogged]
        private static string GetCopilotReference(
            [NotLogged] ProjectItem projectItem,
            string solutionDir
        )
        {
            var result = string.Empty;

            try
            {
                if (projectItem == null)
                    return result;

                if (string.IsNullOrWhiteSpace(solutionDir))
                    return result;

                if (projectItem.FileCount <= 0)
                    return result;

                var fullPath = projectItem.FileNames[1];
                if (string.IsNullOrWhiteSpace(fullPath))
                    return result;

                var relativePath = GetRelativePath(solutionDir, fullPath);
                if (string.IsNullOrWhiteSpace(relativePath))
                    return result;

                // Normalize to forward slashes for Copilot reference format
                relativePath = relativePath.Replace('\\', '/');

                result = $"#file:'{relativePath}'";
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
        /// Computes a relative path from <paramref name="basePath" /> to
        /// <paramref
        ///     name="targetPath" />
        /// .
        /// </summary>
        /// <param name="basePath">
        /// (Required.) A <see cref="T:System.String" /> that contains the base
        /// directory path from which to compute the relative path.
        /// </param>
        /// <param name="targetPath">
        /// (Required.) A <see cref="T:System.String" /> that contains the target
        /// file or directory path for which to compute the relative path.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.String" /> containing the relative path from
        /// <paramref name="basePath" /> to <paramref name="targetPath" />, or
        /// <paramref name="targetPath" /> if a relative path cannot be computed.
        /// </returns>
        /// <remarks>
        /// This method provides a lightweight implementation of
        /// <c>Path.GetRelativePath</c> for .NET Framework 4.8 compatibility, using
        /// <see cref="T:System.Uri" />-based logic.
        /// <para />
        /// If <paramref name="basePath" /> is blank or whitespace, the method
        /// returns <paramref name="targetPath" /> (or
        /// <see
        ///     cref="F:System.String.Empty" />
        /// if <paramref name="targetPath" /> is
        /// also blank).
        /// <para />
        /// If <paramref name="targetPath" /> is blank or whitespace, the method
        /// returns <see cref="F:System.String.Empty" />.
        /// <para />
        /// If the path(s) are on different volume(s) or scheme(s), the method
        /// returns <paramref name="targetPath" /> unchanged.
        /// <para />
        /// If URI construction fails, the method returns
        /// <paramref
        ///     name="targetPath" />
        /// unchanged.
        /// </remarks>
        [return: NotLogged]
        private static string GetRelativePath(
            string basePath,
            string targetPath
        )
        {
            var result = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(basePath))
                    return targetPath ?? string.Empty;

                if (string.IsNullOrWhiteSpace(targetPath))
                    return result;

                // Ensure directories end with separator
                if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    if (!basePath.EndsWith(
                            Path.AltDirectorySeparatorChar.ToString()
                        ))
                    {
                        basePath += Path.DirectorySeparatorChar;
                    }
                }

                var baseUri = new Uri(basePath, UriKind.Absolute);
                if (baseUri == null)
                    return targetPath;

                var targetUri = new Uri(targetPath, UriKind.Absolute);
                if (targetUri == null)
                    return targetPath;

                // Different volume -> cannot make relative
                if (baseUri.Scheme != targetUri.Scheme)
                    return targetPath;

                if (!string.Equals(
                        baseUri.Authority, targetUri.Authority,
                        StringComparison.OrdinalIgnoreCase
                    ))
                    return targetPath;

                var relativeUri = baseUri.MakeRelativeUri(targetUri);
                if (relativeUri == null)
                    return targetPath;

                var relativePath =
                    Uri.UnescapeDataString(relativeUri.ToString());
                if (string.IsNullOrWhiteSpace(relativePath))
                    return targetPath;

                // Convert URI separators to platform separators first, then we will normalize to '/' for Copilot
                relativePath = relativePath.Replace(
                    '/', Path.DirectorySeparatorChar
                );

                result = relativePath;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = targetPath;
            }

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
                if (package == null)
                    return;

                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(
                    package.DisposalToken
                );

                var commandService =
                    await package.GetServiceAsync(typeof(IMenuCommandService))
                        as IMenuCommandService;

                if (commandService == null)
                    return;

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
    }
}