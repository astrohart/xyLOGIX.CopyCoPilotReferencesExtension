using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xyLOGIX.Core.Debug;
using Constants = EnvDTE.Constants;

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
        /// Ensures trailing directory separator (for
        /// <see cref="M:System.Uri.MakeRelativeUri(System.Uri)" /> correctness).
        /// </summary>
        [return: NotLogged]
        private static string AppendDirectorySeparator([NotLogged] string path)
        {
            var result = default(string);
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return result;

                var c = Path.DirectorySeparatorChar;
                if (path[path.Length - 1] == c)
                {
                    result = path;
                    return result;
                }

                result = path + c;
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
        /// Converts <see cref="T:EnvDTE.ProjectItem" /> entries into absolute file system
        /// paths.
        /// </summary>
        /// <param name="projectItems">
        /// Reference to a read-only list of
        /// <see cref="T:EnvDTE.ProjectItem" />.
        /// </param>
        /// <returns>A list of absolute file paths.</returns>
        /// <remarks>
        /// Uses only FileNames[1] for each item; skips non-physical items, folders, or
        /// items without files.
        /// </remarks>
        [return: NotLogged]
        private static List<string> CollectAbsolutePaths(
            [NotLogged] IReadOnlyList<ProjectItem> projectItems
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = new List<string>();
            try
            {
                if (projectItems == null) return result;

                // Use a HashSet to prevent duplicates while preserving first-in order.
                var seen =
                    new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var item in projectItems)
                {
                    if (item == null) continue;

                    var full = ProcessProjectItem(item);
                    if (string.IsNullOrWhiteSpace(full)) continue;

                    if (!seen.Add(full)) continue;

                    result.Add(full);
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = new List<string>(); // default
            }

            return result;
        }

        /// <summary>
        /// Ends the wait dialog if it was shown.
        /// </summary>
        private static void EndWaitDialog(
            [NotLogged] IVsThreadedWaitDialog2 dialog
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                if (dialog == null) return;
                dialog.EndWaitDialog(out _);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }

        /// <summary>
        /// Handles the command execution: obtains selected items, extracts full paths,
        /// converts to solution-relative #file:'…' refs, and copies to the clipboard.
        /// </summary>
        /// <remarks>
        /// Validates all inputs and returns eagerly on invalid state. All DTE access
        /// is kept on the UI thread (STA). Only pure path/formatting work is parallelized.
        /// </remarks>
        private void Execute([NotLogged] object sender, [NotLogged] EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var operationCompleted =
                false; // defensive style for void; indicates success path
            IVsThreadedWaitDialog2 waitDialog = null;

            try
            {
                var dte2 = Package.GetGlobalService(typeof(SDTE)) as DTE2;
                if (dte2 == null) return;

                // Optional: lightweight, non-blocking progress UI with marquee
                waitDialog = StartWaitDialog(
                    "Microsoft Visual Studio",
                    "Collecting selection and formatting paths…", "Working…"
                );

                var projectItems = GetSelectedProjectItems(dte2);
                if (projectItems == null) return;
                if (projectItems.Count == 0) return;

                // Step 1 (UI thread): collect absolute file paths from ProjectItem(s)
                var absolutePaths = CollectAbsolutePaths(projectItems);
                if (absolutePaths == null) return;
                if (absolutePaths.Count == 0) return;

                // Step 2 (UI thread): capture solution directory once
                var solutionDir = GetSolutionDirectory(dte2);
                if (string.IsNullOrWhiteSpace(solutionDir)) return;

                // Step 3 (BG CPU-only): normalize, relativize, and format in parallel
                var formatted = TransformToCopilotReferences(
                    absolutePaths, solutionDir
                );

                // Step 4 (UI thread): join and copy to clipboard
                var text = JoinReferences(formatted);
                if (string.IsNullOrWhiteSpace(text)) return;

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
                _ = operationCompleted; // keep local for diagnostics if needed
            }
        }

        /// <summary>
        /// Formats a path as a Copilot Chat file reference token.
        /// </summary>
        [return: NotLogged]
        private static string FormatCopilotReference(
            [NotLogged] string solutionRelativePath
        )
        {
            var result = default(string);
            try
            {
                if (string.IsNullOrWhiteSpace(solutionRelativePath))
                    return result;
                result = "#file:'" + solutionRelativePath + "'";
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

            var result = new List<ProjectItem>();
            try
            {
                if (dte2 == null) return result;

                var selected = dte2.SelectedItems;
                if (selected != null)
                    foreach (SelectedItem sel in selected)
                    {
                        if (sel == null) continue;

                        // Pattern matching + eager-continue
                        if (sel.ProjectItem is ProjectItem pi)
                        {
                            result.Add(pi);
                            continue;
                        }

                        // If a project (not a file) is selected, skip; this command targets files.
                        if (sel.Project != null) continue;
                    }

                if (result.Count > 0) return result;

                // Fallback to ActiveDocument.ProjectItem (when invoked from an editor tab)
                var ad = dte2.ActiveDocument;
                if (ad == null) return result;

                if (ad.ProjectItem is ProjectItem activePi)
                    result.Add(activePi);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = new List<ProjectItem>(); // default
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

            var result = default(string);
            try
            {
                if (dte2 == null) return result;
                var sln = dte2.Solution;
                if (sln == null) return result;

                var slnPath = sln.FullName;
                if (string.IsNullOrWhiteSpace(slnPath)) return result;

                var dir = Path.GetDirectoryName(slnPath);
                if (string.IsNullOrWhiteSpace(dir)) return result;

                result = dir;
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
        /// Extracts the absolute path for a single <see cref="T:EnvDTE.ProjectItem" /> if
        /// it is a physical file.
        /// </summary>
        /// <param name="projectItem">Reference to a <see cref="T:EnvDTE.ProjectItem" />.</param>
        /// <returns>The absolute file path, or <see langword="null" /> on failure.</returns>
        /// <remarks>
        /// Skips non-physical items and items with no files. Bounds-checks FileCount and
        /// FileNames[1].
        /// </remarks>
        [return: NotLogged]
        private static string ProcessProjectItem(
            [NotLogged] ProjectItem projectItem
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = default(string);
            try
            {
                if (projectItem == null) return result;

                // Only physical files
                if (!string.Equals(
                        projectItem.Kind,
                        Constants.vsProjectItemKindPhysicalFile,
                        StringComparison.Ordinal
                    ))
                    return result;

                if (projectItem.FileCount < 1) return result;

                // 1-based index
                var candidate = projectItem.FileNames[1];
                if (string.IsNullOrWhiteSpace(candidate)) return result;

                // Ensure absolute and existent path
                if (!Path.IsPathRooted(candidate)) return result;
                if (!File.Exists(candidate)) return result;

                result = candidate;
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
        /// Shows the VS Threaded Wait Dialog with a marquee progress bar.
        /// </summary>
        [return: NotLogged]
        private static IVsThreadedWaitDialog2 StartWaitDialog(
            [NotLogged] string caption,
            [NotLogged] string message,
            [NotLogged] string status
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsThreadedWaitDialog2 result = null;
            try
            {
                var factory =
                    Package.GetGlobalService(
                        typeof(SVsThreadedWaitDialogFactory)
                    ) as IVsThreadedWaitDialogFactory;
                if (factory == null) return result;

                factory.CreateInstance(out result);
                if (result == null) return result;

                // Cancel disabled; marquee enabled
                result.StartWaitDialog(
                    caption, message, status, null, null, 0, false, true
                );
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Converts an absolute path to a solution-relative path.
        /// </summary>
        /// <param name="absolutePath">Reference to an absolute file path.</param>
        /// <param name="solutionDir">Reference to the absolute solution directory.</param>
        /// <returns>A relative path using forward slashes, or <see langword="null" />.</returns>
        [return: NotLogged]
        private static string ToSolutionRelative(
            [NotLogged] string absolutePath,
            [NotLogged] string solutionDir
        )
        {
            var result = default(string);
            try
            {
                if (string.IsNullOrWhiteSpace(absolutePath)) return result;
                if (string.IsNullOrWhiteSpace(solutionDir)) return result;

                var baseUri = new Uri(
                    AppendDirectorySeparator(solutionDir), UriKind.Absolute
                );
                var fileUri = new Uri(absolutePath, UriKind.Absolute);

                var rel = baseUri.MakeRelativeUri(fileUri)
                                 .ToString(); // already forward slashes
                if (string.IsNullOrWhiteSpace(rel)) return result;

                result = rel;
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
        /// Transforms absolute paths into solution-relative Copilot Chat references in
        /// parallel.
        /// </summary>
        /// <param name="absolutePaths">Reference to a list of absolute paths.</param>
        /// <param name="solutionDir">Reference to the absolute solution directory.</param>
        /// <returns>A read-only list of formatted <c>#file:'…'</c> tokens.</returns>
        [return: NotLogged]
        private static IReadOnlyList<string> TransformToCopilotReferences(
            [NotLogged] IReadOnlyList<string> absolutePaths,
            [NotLogged] string solutionDir
        )
        {
            var result = Array.Empty<string>();

            try
            {
                if (absolutePaths == null) return result;
                if (string.IsNullOrWhiteSpace(solutionDir)) return result;

                var count = absolutePaths.Count;
                if (count < 1) return result;

                var output = new string[count];

                // Do string CPU work off the UI thread.
                Parallel.For(
                    0, count, i =>
                    {
                        string local = null;
                        try
                        {
                            var abs = absolutePaths[i];
                            if (!string.IsNullOrWhiteSpace(abs))
                            {
                                var rel = ToSolutionRelative(abs, solutionDir);
                                if (!string.IsNullOrWhiteSpace(rel))
                                {
                                    var norm = NormalizeSeparators(rel);
                                    local = FormatCopilotReference(norm);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // dump all the exception info to the log
                            DebugUtils.LogException(ex);
                            local = null;
                        }

                        output[i] = local;
                    }
                );

                // Condense null/empty entries while preserving order
                var compact = new List<string>(output.Length);
                for (var i = 0; i < output.Length; i++)
                {
                    var s = output[i];
                    if (string.IsNullOrWhiteSpace(s)) continue;
                    compact.Add(s);
                }

                result = compact.ToArray();
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
                result = Array.Empty<string>();
            }

            return result;
        }

        /// <summary>
        /// Copies text to the clipboard on the UI thread.
        /// </summary>
        private static void TryCopyToClipboard([NotLogged] string text)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                if (string.IsNullOrWhiteSpace(text)) return;
                Clipboard.SetText(text);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);
            }
        }
    }
}