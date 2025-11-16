using CCPRE.Generators.References.Constants;
using CCPRE.Generators.References.Interfaces;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Debug;
using xyLOGIX.Core.Files;

namespace CCPRE.Generators.References
{
    /// <summary>
    /// Provides a base implementation of
    /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
    /// using the Template Method pattern.
    /// </summary>
    /// <remarks>
    /// This abstract class provides common functionality for all Copilot reference
    /// generator strategy(ies), including input validation and error handling.
    /// <para />
    /// Derived class(es) must override the
    /// <see
    ///     cref="M:CCPRE.Generators.References.CopilotReferenceGeneratorBase.OnGenerate(System.Object,System.String)" />
    /// method to provide type-specific reference generation logic.
    /// </remarks>
    public abstract class
        CopilotReferenceGeneratorBase : ICopilotReferenceGenerator
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CCPRE.Generators.References.CopilotReferenceGeneratorBase" />
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
        static CopilotReferenceGeneratorBase() { }

        /// <summary>
        /// Initializes a new instance of
        /// <see cref="T:CCPRE.Generators.References.CopilotReferenceGeneratorBase" /> and
        /// returns a reference to it.
        /// </summary>
        /// <remarks>
        /// <strong>NOTE:</strong> This constructor is marked
        /// <see langword="protected" /> due to the fact that this class is marked
        /// <see langword="abstract" />.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        protected CopilotReferenceGeneratorBase() { }

        /// <summary>
        /// Gets a
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// value that indicates which type of generator this instance represents.
        /// </summary>
        public abstract CopilotReferenceGeneratorType GeneratorType
        {
            [DebuggerStepThrough] get;
        }

        /// <summary>
        /// Generates a GitHub Copilot reference string from the specified Visual
        /// Studio object.
        /// </summary>
        /// <param name="selectedObject">
        /// (Required.) Reference to an instance of <see cref="T:System.Object" />
        /// that represents the selected Visual Studio element (e.g.,
        /// <see cref="T:EnvDTE.ProjectItem" />, <see cref="T:EnvDTE.Project" />,
        /// or CodeElement).
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
        /// This method validates all input(s) and returns
        /// <see cref="F:System.String.Empty" /> if <paramref name="selectedObject" />
        /// is <see langword="null" />.
        /// <para />
        /// This method returns <see cref="F:System.String.Empty" /> if
        /// <paramref name="solutionDirectory" /> is blank or whitespace.
        /// <para />
        /// This method delegates to the
        /// <see
        ///     cref="M:CCPRE.Generators.References.CopilotReferenceGeneratorBase.OnGenerate(System.Object,System.String)" />
        /// template method after validation.
        /// </remarks>
        [return: NotLogged]
        public string Generate(
            [NotLogged] object selectedObject,
            [NotLogged] string solutionDirectory
        )
        {
            var result = string.Empty;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopilotReferenceGeneratorBase.Generate: Checking whether the method parameter, 'selectedObject', has a null reference for a value..."
                );

                // Check to see if the required parameter, 'selectedObject', is null. If it is,
                // then write an error message to the log file and then terminate the
                // execution of this method, returning the default return value.
                if (selectedObject == null)
                {
                    // The method parameter, 'selectedObject', is required and is not supposed
                    // to have a NULL value.  It does, and this is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "CopilotReferenceGeneratorBase.Generate: *** ERROR *** A null reference was passed for the method parameter, 'selectedObject'.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopilotReferenceGeneratorBase.Generate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopilotReferenceGeneratorBase.Generate: *** SUCCESS *** We have been passed a valid object reference for the method parameter, 'selectedObject'.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopilotReferenceGeneratorBase.Generate *** INFO: Checking whether the folder with path, '{solutionDirectory}', exists on the file system..."
                );

                // Check whether a folder having the path, 'solutionDirectory', exists on the file system.
                // If it does not, then write an error message to the log file, and then terminate
                // the execution of this method, returning the default return value.
                if (!Does.FolderExist(solutionDirectory))
                {
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"CopilotReferenceGeneratorBase.Generate: *** ERROR *** The system could not locate the folder having the path, '{solutionDirectory}', on the file system.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** CopilotReferenceGeneratorBase.Generate: Result = '{result}'"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopilotReferenceGeneratorBase.Generate: *** SUCCESS *** The folder with path, '{solutionDirectory}', was found on the file system.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"*** FYI *** Calling the OnGenerate() method to invoke the custom processing for the '{GeneratorType}' generator type..."
                );

                result = OnGenerate(selectedObject, solutionDirectory);
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = string.Empty;
            }

            DebugUtils.WriteLine(
                DebugLevel.Debug,
                $"CopilotReferenceGeneratorBase.Generate: Result = '{result}'"
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
        /// This method is called by the
        /// <see
        ///     cref="M:CCPRE.Generators.References.CopilotReferenceGeneratorBase.Generate(System.Object,System.String)" />
        /// method after input validation has been performed.
        /// <para />
        /// Implementer(s) can assume that <paramref name="selectedObject" /> is
        /// not <see langword="null" /> and that <paramref name="solutionDirectory" />
        /// is not blank or whitespace.
        /// <para />
        /// Implementer(s) should return <see cref="F:System.String.Empty" /> if
        /// the reference cannot be generated for any reason.
        /// </remarks>
        [return: NotLogged]
        protected abstract string OnGenerate(
            [NotLogged] object selectedObject,
            [NotLogged] string solutionDirectory
        );
    }
}