using CCPRE.Generators.References.Constants;
using CCPRE.Generators.References.Interfaces;
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
        /// <see cref="T:CCPRE.Generators.References.FileCopilotReferenceGenerator" /> and
        /// returns a reference to it.
        /// </summary>
        /// <remarks>
        /// This is an empty, <see langword="private" /> constructor to prohibit direct
        /// allocation of this class, as it is a <c>Singleton</c> object accessible via the
        /// <see
        ///     cref="P:CCPRE.Generators.References.FileCopilotReferenceGenerator.Instance" />
        /// property.
        /// </remarks>
        [Log(AttributeExclude = true)]
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
        /// Gets a reference to the one and only instance of the object that implements the
        /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// interface for the
        /// <see
        ///     cref="F:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.File" />
        /// <c>Copilot Reference Generator</c> type.
        /// </summary>
        public static ICopilotReferenceGenerator Instance
        {
            [DebuggerStepThrough] get;
        } = new FileCopilotReferenceGenerator();

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
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Attempting to normalize the path separators in the path, '{relativePath}'..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.NormalizeSeparators: Checking whether the variable, 'relativePath', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'relativePath', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    // the variable relativePath is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.NormalizeSeparators: *** ERROR *** The variable, 'relativePath', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.NormalizeSeparators: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.NormalizeSeparators: *** SUCCESS *** {relativePath.Length} B of data appear to be present in the variable, 'relativePath'.  Proceeding..."
                );

                result = relativePath.Replace('\\', '/');
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"FileCopilotReferenceGenerator.NormalizeSeparators: Result = '{result}'"
            );

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
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FileCopilotReferenceGenerator.OnGenerate: Checking whether the selected item is a ProjectItem..."
                );

                // Check to see whether the selected item is a ProjectItem.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!(selectedObject is ProjectItem projectItem))
                {
                    // The selected item is NOT a ProjectItem.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The selected item is NOT a ProjectItem.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** The selected item is a ProjectItem.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FileCopilotReferenceGenerator.OnGenerate: Checking whether the current ProjectItem represents a physical file..."
                );

                // Check to see whether the current ProjectItem represents a physical file.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!ProjectItemRepresentsPhysicalFile(projectItem))
                {
                    // The current ProjectItem does NOT represent a physical file.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The current ProjectItem does NOT represent a physical file.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** The current ProjectItem represents a physical file.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: Checking whether the FileCount property of the current ProjectItem is greater than, or equal to, one (1)..."
                );

                // Check to see whether the FileCount property of the current ProjectItem is greater than, or equal to, one (1).
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (projectItem.FileCount < 1)
                {
                    // The FileCount property of the current ProjectItem is less than one (1).  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The FileCount property of the current ProjectItem is less than one (1).  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** The FileCount property of the current ProjectItem is greater than, or equal to, one (1).  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to retrieve the absolute path of the current ProjectItem from the FileNames property..."
                );

                var absolutePath = projectItem.FileNames[1];

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: Checking whether the variable, 'absolutePath', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'absolutePath', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(absolutePath))
                {
                    // the variable absolutePath is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.OnGenerate: *** ERROR *** The variable, 'absolutePath', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** {absolutePath.Length} B of data appear to be present in the variable, 'absolutePath'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FileCopilotReferenceGenerator.OnGenerate: Checking whether the pathname we obtained is an absolute path..."
                );

                // Check to see whether the pathname we obtained is an absolute path.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!absolutePath.IsAbsolutePath())
                {
                    // The pathname we obtained is an absolute path.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The pathname we obtained is an absolute path.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** The pathname we obtained is an absolute path.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FileCopilotReferenceGenerator.OnGenerate: Checking whether the pathname we obtained contains a root..."
                );

                // Check to see whether the pathname we obtained contains a root.
                // If this is not the case, then write an error message to the log file,
                // and then terminate the execution of this method.
                if (!Path.IsPathRooted(absolutePath))
                {
                    // The pathname we obtained does NOT contain a root.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The pathname we obtained does NOT contain a root.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** The pathname we obtained contains a root.  Proceeding..."
                );

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
                        $"*** FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
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
                    "*** FYI *** Attempting to convert the absolute path to a solution-relative path..."
                );

                var relativePath = ToSolutionRelative(
                    absolutePath, solutionDirectory
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: Checking whether the variable, 'relativePath', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'relativePath', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    // the variable relativePath is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.OnGenerate: *** ERROR *** The variable, 'relativePath', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** {relativePath.Length} B of data appear to be present in the variable, 'relativePath'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to normalize the path separators for GitHub Copilot compatibility..."
                );

                var normalized = NormalizeSeparators(relativePath);

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.OnGenerate: Checking whether the variable, 'normalized', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'normalized', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(normalized))
                {
                    // the variable normalized is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.OnGenerate: *** ERROR *** The variable, 'normalized', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.OnGenerate: *** SUCCESS *** {normalized.Length} B of data appear to be present in the variable, 'normalized'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Formulating the final GitHub Copilot reference..."
                );

                result = $"#file:'{normalized}'";
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"FileCopilotReferenceGenerator.OnGenerate: Result = '{result}'"
            );

            return result;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="projectItem" /> represents a
        /// physical file in the corresponding <c>Project</c>.
        /// </summary>
        /// <param name="projectItem">
        /// (Required.) Reference to an instance of an object that implements the
        /// <see cref="T:EnvDTE.ProjectItem" /> interface that is to be evaluated.
        /// <para />
        /// A <see langword="null" /> reference may not be passed for the argument of this
        /// parameter.
        /// </param>
        /// <remarks>
        /// This method checks the value of the
        /// <see cref="P:EnvDTE.ProjectItem.Kind" /> property of the specified
        /// <paramref name="projectItem" /> to determine if it matches the constant value
        /// <see cref="F:EnvDTE.Constants.vsProjectItemKindPhysicalFile" />.
        /// <para />
        /// If the argument of the <paramref name="projectItem" /> parameter is set to a
        /// <see langword="null" /> reference, or if the value of the
        /// <see cref="P:EnvDTE.ProjectItem.Kind" /> property is set to a
        /// <see langword="null" /> <see cref="T:System.String" />, a blank
        /// <see cref="T:System.String" />, or the <see cref="F:System.String.Empty" />
        /// value, then the method returns <see langword="false" />.
        /// </remarks>
        /// <returns>
        /// <see langword="true" /> if the <paramref name="projectItem" />
        /// represents a physical file; otherwise, <see langword="false" />.
        /// </returns>
        private static bool ProjectItemRepresentsPhysicalFile(
            [NotLogged] ProjectItem projectItem
        )
        {
            var result = false;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: Checking whether the method parameter, 'projectItem', has a null reference for a value..."
                );

                // Check to see if the required parameter, 'projectItem', is null. If it is,
                // then write an error message to the log file and then terminate the
                // execution of this method, returning the default return value.
                if (projectItem == null)
                {
                    // The method parameter, 'projectItem', is required and is not supposed
                    // to have a NULL value.  It does, and this is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: *** ERROR *** A null reference was passed for the method parameter, 'projectItem'.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: *** SUCCESS *** We have been passed a valid object reference for the method parameter, 'projectItem'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** INFO: Checking whether the property, 'projectItem.Kind', appears to have a null or blank value..."
                );

                // Check to see if the required property, 'projectItem.Kind', appears to have a null 
                // or blank value. If it does, then send an error to the log file and quit,
                // returning the default value of the result variable.
                if (string.IsNullOrWhiteSpace(projectItem.Kind))
                {
                    // The property, 'projectItem.Kind', appears to have a null or blank value.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR: The property, 'projectItem.Kind', appears to have a null or blank value.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** SUCCESS *** The property, 'projectItem.Kind', seems to have a non-blank value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Checking whether the value of the property, 'projectItem.Kind' ('{projectItem.Kind}'), equals the constant value, 'vsProjectItemKindPhysicalFile' ('{EnvDTE.Constants.vsProjectItemKindPhysicalFile}')..."
                );

                result =
                    EnvDTE.Constants.vsProjectItemKindPhysicalFile.EqualsNoCase(
                        projectItem.Kind, StringComparison.Ordinal
                    );
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = false;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"FileCopilotReferenceGenerator.ProjectItemRepresentsPhysicalFile: Result = {result}"
            );

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
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** The folder with path, '{solutionDirectory}', was found on the file system.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Attempting to create URIs for the solution directory and the absolute path..."
                );

                var baseUri = new Uri(
                    AppendDirectorySeparator(solutionDirectory),
                    UriKind.Absolute
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: Checking whether the variable, 'baseUri == null', has a null reference for a value..."
                );

                // Check to see if the variable, baseUri == null, is null.  If it is, send an error
                // to the log file and terminate the execution of this method, returning
                // the default return value.
                if (baseUri == null == null)
                {
                    // the variable baseUri == null is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.ToSolutionRelative: *** ERROR ***  The variable, 'baseUri == null', has a null reference.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, baseUri == null, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** The variable, 'baseUri == null', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Formulating the URI for the absolute path..."
                );

                var fileUri = new Uri(absolutePath, UriKind.Absolute);

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: Checking whether the variable, 'fileUri', has a null reference for a value..."
                );

                // Check to see if the variable, fileUri, is null.  If it is, send an error
                // to the log file and terminate the execution of this method, returning
                // the default return value.
                if (fileUri == null)
                {
                    // the variable fileUri is required to have a valid object reference.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.ToSolutionRelative: *** ERROR ***  The variable, 'fileUri', has a null reference.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                // We can use the variable, fileUri, because it's not set to a null reference.
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** The variable, 'fileUri', has a valid object reference for its value.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "*** FYI *** Formulating the solution-relative path..."
                );

                var relative = baseUri.MakeRelativeUri(fileUri)
                                      .ToString();

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "FileCopilotReferenceGenerator.ToSolutionRelative: Checking whether the variable, 'relative', has a null reference for a value, or is blank..."
                );

                // Check to see if the required variable, 'relative', is null or blank. If it is, 
                // then send an  error to the log file and quit, returning the default value 
                // of the result variable.
                if (string.IsNullOrWhiteSpace(relative))
                {
                    // the variable relative is required.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "FileCopilotReferenceGenerator.ToSolutionRelative: *** ERROR *** The variable, 'relative', has a null reference or is blank.  Stopping..."
                    );

                    // log the result
                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"FileCopilotReferenceGenerator.ToSolutionRelative: *** SUCCESS *** {relative.Length} B of data appear to be present in the variable, 'relative'.  Proceeding..."
                );

                result = relative;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"FileCopilotReferenceGenerator.ToSolutionRelative: Result = '{result}'"
            );

            return result;
        }
    }
}