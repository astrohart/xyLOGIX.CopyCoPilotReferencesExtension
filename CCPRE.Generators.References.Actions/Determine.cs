using CCPRE.Generators.References.Constants;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PostSharp.Patterns.Diagnostics;
using System;
using xyLOGIX.Core.Debug;

namespace CCPRE.Generators.References.Actions
{
    /// <summary>
    /// Provides method(s) for determining which
    /// <see
    ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
    /// strategy to use based on Visual Studio selection(s).
    /// </summary>
    public static class Determine
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CCPRE.Generators.References.Actions.Determine" />
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
        static Determine() { }

        /// <summary>
        /// Determines the appropriate
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// to use based on the specified Visual Studio object.
        /// </summary>
        /// <param name="selectedObject">
        /// (Required.) Reference to an instance of <see cref="T:System.Object" />
        /// that represents the selected Visual Studio element.
        /// </param>
        /// <returns>
        /// A
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// value that indicates which strategy should be used to generate the
        /// Copilot reference, or
        /// <see
        ///     cref="F:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown" />
        /// if the type cannot be determined.
        /// </returns>
        /// <remarks>
        /// This method inspects the type and properties of
        /// <paramref name="selectedObject" /> to determine which generator strategy
        /// is appropriate.
        /// <para />
        /// This method returns
        /// <see
        ///     cref="F:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown" />
        /// if <paramref name="selectedObject" /> is <see langword="null" />.
        /// <para />
        /// This method returns
        /// <see
        ///     cref="F:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown" />
        /// if <paramref name="selectedObject" /> is of an unrecognized type.
        /// </remarks>
        public static CopilotReferenceGeneratorType
            TheReferenceGenerationStrategyToUse(
                [NotLogged] object selectedObject
            )
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = CopilotReferenceGeneratorType.Unknown;

            try
            {
                if (selectedObject == null)
                    return result;

                if (selectedObject is Project)
                    return CopilotReferenceGeneratorType.Project;

                if (selectedObject is ProjectItem projectItem)
                    return !AskWhether.ProjectItemRepresentsPhysicalFile(
                        projectItem
                    )
                        ? result
                        : CopilotReferenceGeneratorType.File;

                // Future: Add CodeElement inspection for class/method/property/field/event/constant
                // Example:
                // if (selectedObject is CodeClass)
                // {
                //     result = CopilotReferenceGeneratorType.Class;
                //     return result;
                // }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = CopilotReferenceGeneratorType.Unknown;
            }

            return result;
        }
    }
}