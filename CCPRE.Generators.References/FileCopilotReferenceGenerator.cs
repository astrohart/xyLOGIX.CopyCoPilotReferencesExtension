using CCPRE.Generators.References.Constants;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using System.IO;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Extensions;
using xyLOGIX.Core.Files;

namespace CCPRE.Generators.References
{
    /// <summary>
    /// Generates GitHub Copilot <c>#file:'path'</c> reference(s) for
    /// <see cref="T:EnvDTE.ProjectItem" /> object(s).
    /// </summary>
    /// <remarks>
    /// This class implements the
    /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
    /// interface for file-based reference(s).
    /// <para />
    /// This is a singleton class; call
    /// <see
    ///     cref="P:CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance" />
    /// to obtain a reference to the sole instance.
    /// </remarks>
    public class FileCopilotReferenceGenerator : CopilotReferenceGeneratorBase
    {
        /// <summary>
        /// Reference to an instance of
        /// <see cref="T:CCPRE.Generators.References.FileCopilotReferenceGenerator" />
        /// that is the sole instance of this class.
        /// </summary>
        /// <remarks>
        /// <b>NOTE:</b> The purpose of this field is to cache the value of the
        /// <see
        ///     cref="P:CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance" />
        /// property.
        /// </remarks>
        private static FileCopilotReferenceGenerator _instance;

        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CCPRE.Generators.References.FileCopilotReferenceGenerator" />
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
        static FileCopilotReferenceGenerator() { }

        /// <summary>
        /// Constructs a new instance of
        /// <see cref="T:CCPRE.Generators.References.FileCopilotReferenceGenerator" />
        /// and returns a reference to it.
        /// </summary>
        /// <remarks>
        /// This constructor is <see langword="private" /> to enforce the singleton
        /// pattern.
        /// </remarks>
        private FileCopilotReferenceGenerator() { }

        /// <summary>
        /// Gets a
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// value that indicates which type of generator this instance represents.
        /// </summary>
        public override CopilotReferenceGeneratorType GeneratorType
        {
            [DebuggerStepThrough] get => CopilotReferenceGeneratorType.File;
        }

        /// <summary>
        /// Gets a reference to the sole instance of
        /// <see cref="T:CCPRE.Generators.References.FileCopilotReferenceGenerator" />.
        /// </summary>
        public static FileCopilotReferenceGenerator Instance
        {
            [DebuggerStepThrough]
            get
                => _instance ??
                   (_instance = new FileCopilotReferenceGenerator());
        }

        /// <summary>
        /// Normalizes path separator(s) to forward slash(es) for GitHub Copilot
        /// compatibility.
        /// </summary>
        /// <param name="relativePath">
        /// (Required.) A <see cref="T:System.String" /> that contains the relative
        /// path to normalize.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.String" /> containing the normalized path with
        /// forward slash(es), or <see cref="F:System.String.Empty" /> if
        /// <paramref name="relativePath" /> is blank or whitespace.
        /// </returns>
        /// <remarks>
        /// This method returns <see cref="F:System.String.Empty" /> if
        /// <paramref name="relativePath" /> is blank or whitespace.
        /// </remarks>
        [return: NotLogged]
        private static string NormalizeSeparators(
            [NotLogged] string relativePath
        )
        {
            var result = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                    return result;

                result = relativePath.Replace('\\', '/');
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
        /// When overridden in a derived class, generates a GitHub Copilot reference
        /// string from the specified Visual Studio object.
        /// </summary>
        /// <param name="selectedObject">
        /// (Required.) Reference to an instance of <see cref="T:System.Object" />
        /// that represents the selected Visual Studio element.
        /// </param>
        /// <param name="solutionDirectory">
        /// (Required.) A <see cref="T:System.String" /> that contains the absolute
        /// path to the solution directory.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.String" /> containing the formatted GitHub Copilot
        /// reference, or <see cref="F:System.String.Empty" /> if the reference
        /// cannot be generated.
        /// </returns>
        /// <remarks>
        /// This method expects <paramref name="selectedObject" /> to be an instance
        /// of <see cref="T:EnvDTE.ProjectItem" /> representing a physical file.
        /// <para />
        /// This method returns <see cref="F:System.String.Empty" /> if
        /// <paramref name="selectedObject" /> is not a
        /// <see cref="T:EnvDTE.ProjectItem" />.
        /// <para />
        /// This method returns <see cref="F:System.String.Empty" /> if the
        /// <see cref="T:EnvDTE.ProjectItem" /> does not represent a physical file.
        /// </remarks>
        [return: NotLogged]
        protected override string OnGenerate(
            [NotLogged] object selectedObject,
            [NotLogged] string solutionDirectory
        )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = string.Empty;

            try
            {
                if (!(selectedObject is ProjectItem projectItem))
                    return result;

                if (!string.Equals(
                        projectItem.Kind,
                        EnvDTE.Constants.vsProjectItemKindPhysicalFile,
                        StringComparison.Ordinal
                    ))
                    return result;

                if (projectItem.FileCount < 1)
                    return result;

                var absolutePath = projectItem.FileNames[1];
                if (string.IsNullOrWhiteSpace(absolutePath))
                    return result;

                if (!Path.IsPathRooted(absolutePath))
                    return result;

                if (!File.Exists(absolutePath))
                    return result;

                var relativePath = ToSolutionRelative(
                    absolutePath, solutionDirectory
                );
                if (string.IsNullOrWhiteSpace(relativePath))
                    return result;

                var normalized = NormalizeSeparators(relativePath);
                if (string.IsNullOrWhiteSpace(normalized))
                    return result;

                result = $"#file:'{normalized}'";
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
        /// Converts an absolute file path to a solution-relative path.
        /// </summary>
        /// <param name="absolutePath">
        /// (Required.) A <see cref="T:System.String" /> that contains the absolute
        /// file path to convert.
        /// </param>
        /// <param name="solutionDirectory">
        /// (Required.) A <see cref="T:System.String" /> that contains the absolute
        /// path to the solution directory.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.String" /> containing the solution-relative path,
        /// or <see cref="F:System.String.Empty" /> if the conversion cannot be
        /// performed.
        /// </returns>
        /// <remarks>
        /// This method returns <see cref="F:System.String.Empty" /> if
        /// <paramref name="absolutePath" /> is blank or whitespace.
        /// <para />
        /// This method returns <see cref="F:System.String.Empty" /> if
        /// <paramref name="solutionDirectory" /> is blank or whitespace.
        /// </remarks>
        [return: NotLogged]
        private static string ToSolutionRelative(
            [NotLogged] string absolutePath,
            [NotLogged] string solutionDirectory
        )
        {
            var result = string.Empty;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** INFO: Checking whether the file with path '{absolutePath}' exists on the file system..."
                );

                // Check whether a folder having the path, 'absolutePath', exists on the file system.
                // If it does not, then write an error message to the log file, and then terminate
                // the execution of this method, returning the default return value.
                if (!Does.FileExist(absolutePath))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"*** ERROR *** The system could not locate the file having the path '{absolutePath}' on the file system.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** SUCCESS *** The file with path '{absolutePath}' was found on the file system.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FileCopilotReferenceGenerator.ToSolutionRelative: Checking whether the pathname specified for the value of the 'absolutePath' parameter is, indeed, an absolute pathname..."
                );

                // Check to see whether the pathname specified for the value of the 'absolutePath' parameter is, indeed, an absolute pathname.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!absolutePath.IsAbsolutePath())
                {
                    // The pathname specified for the value of the 'absolutePath' parameter is NOT an absolute pathname.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The pathname specified for the value of the 'absolutePath' parameter is NOT an absolute pathname.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** The pathname specified for the value of the 'absolutePath' parameter is, indeed, an absolute pathname.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.ToSolutionRelative *** INFO: Checking whether the folder with path, '{solutionDirectory}', exists on the file system..."
                );

                // Check whether a folder having the path, 'solutionDirectory', exists on the file system.
                // If it does not, then write an error message to the log file, and then terminate
                // the execution of this method, returning the default return value.
                if (!Does.FolderExist(solutionDirectory))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"FileCopilotReferenceGenerator.ToSolutionRelative: *** ERROR *** The system could not locate the folder having the path, '{solutionDirectory}', on the file system.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug, $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** The folder with path, '{solutionDirectory}', was found on the file system.  Proceeding..."
                );

                var baseUri = new Uri(
                    AppendDirectorySeparator(solutionDirectory),
                    UriKind.Absolute
                );
                var fileUri = new Uri(absolutePath, UriKind.Absolute);

                var relative = baseUri.MakeRelativeUri(fileUri)
                                      .ToString();
                if (string.IsNullOrWhiteSpace(relative))
                    return result;

                result = relative;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            return result;
        }
    }
}