using EnvDTE;
using PostSharp.Patterns.Diagnostics;
using System;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Extensions;

namespace CCPRE.Generators.References.Actions
{
    /// <summary>
    /// Provides utility method(s) for evaluating and determining specific
    /// characteristics of <see cref="T:EnvDTE.ProjectItem" />(s).
    /// </summary>
    /// <remarks>
    /// The <see cref="T:CCPRE.Generators.References.Actions.AskWhether" />
    /// class contains static methods designed to assist in analyzing
    /// <see cref="T:EnvDTE.ProjectItem" />(s), such as determining whether a given
    /// <see cref="T:EnvDTE.ProjectItem" />represents a physical file.
    /// <para />
    /// This class is not intended to be instantiated.
    /// </remarks>
    public static class AskWhether
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CCPRE.Generators.References.Actions.AskWhether" /> class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any <see langword="static" /> members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static AskWhether() { }

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
        public static bool ProjectItemRepresentsPhysicalFile(
            [NotLogged] ProjectItem projectItem
        )
        {
            var result = false;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "AskWhether.ProjectItemRepresentsPhysicalFile: Checking whether the method parameter, 'projectItem', has a null reference for a value..."
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
                        "AskWhether.ProjectItemRepresentsPhysicalFile: *** ERROR *** A null reference was passed for the method parameter, 'projectItem'.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** AskWhether.ProjectItemRepresentsPhysicalFile: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "AskWhether.ProjectItemRepresentsPhysicalFile: *** SUCCESS *** We have been passed a valid object reference for the method parameter, 'projectItem'.  Proceeding..."
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
                        $"AskWhether.ProjectItemRepresentsPhysicalFile: Result = {result}"
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
                $"AskWhether.ProjectItemRepresentsPhysicalFile: Result = {result}"
            );

            return result;
        }
    }
}