namespace CCPRE.Generators.References.Constants
{
    /// <summary>
    /// Enumerates the different type(s) of Copilot reference generator(s)
    /// supported by the system.
    /// </summary>
    /// <remarks>
    /// Each value corresponds to a specific strategy for generating GitHub
    /// Copilot reference(s) based on the type of Visual Studio element selected.
    /// </remarks>
    public enum CopilotReferenceGeneratorType
    {
        /// <summary>
        /// Indicates that the generator should create a class reference in the
        /// format <c>#codebase</c> or symbol-specific reference.
        /// </summary>
        Class,

        /// <summary>
        /// Indicates that the generator should create a constant reference.
        /// </summary>
        Constant,

        /// <summary>
        /// Indicates that the generator should create an event reference.
        /// </summary>
        Event,

        /// <summary>
        /// Indicates that the generator should create a field reference.
        /// </summary>
        Field,

        /// <summary>
        /// Indicates that the generator should create a file reference in the
        /// format <c>#file:'path'</c>.
        /// </summary>
        File,

        /// <summary>
        /// Indicates that the generator should create a project reference.
        /// </summary>
        Project,

        /// <summary>
        /// Indicates that the generator should create a method reference.
        /// </summary>
        Method,

        /// <summary>
        /// Indicates that the generator should create a property reference.
        /// </summary>
        Property,

        /// <summary>
        /// Indicates that the generator type is unknown or invalid.
        /// </summary>
        Unknown = -1
    }
}