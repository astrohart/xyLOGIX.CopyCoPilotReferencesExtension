using CCPRE.Generators.References.Constants;
using PostSharp.Patterns.Diagnostics;
using System.Diagnostics;

namespace CCPRE.Generators.References.Interfaces
{
    /// <summary>
    /// Defines the publicly-exposed events, methods and properties of an object
    /// that generates GitHub Copilot reference(s) from Visual Studio selection(s).
    /// </summary>
    public interface ICopilotReferenceGenerator
    {
        /// <summary>
        /// Gets a
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// value that indicates which type of generator this instance represents.
        /// </summary>
        CopilotReferenceGeneratorType GeneratorType
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
        /// Implementations must handle Visual Studio COM exceptions gracefully
        /// and return <see cref="F:System.String.Empty" /> on error(s).
        /// </remarks>
        [return: NotLogged]
        string Generate(
            [NotLogged] object selectedObject,
            [NotLogged] string solutionDirectory
        );
    }
}