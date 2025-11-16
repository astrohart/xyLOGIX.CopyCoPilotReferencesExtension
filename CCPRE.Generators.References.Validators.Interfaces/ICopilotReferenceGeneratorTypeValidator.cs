using CCPRE.Generators.References.Constants;

namespace CCPRE.Generators.References.Validators.Interfaces
{
    /// <summary>
    /// Defines the publicly-exposed events, methods and properties of a validator of
    /// <see
    ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
    /// enumeration values.
    /// </summary>
    /// <remarks>
    /// Specifically, objects that implement this interface ascertain whether the
    /// values of variables fall within the value set that is defined by the
    /// <see
    ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
    /// enumeration.
    /// </remarks>
    public interface ICopilotReferenceGeneratorTypeValidator
    {
        /// <summary>
        /// Determines whether the copilot reference generator <paramref name="type" />
        /// value passed is within the value set that is defined by the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// enumeration.
        /// </summary>
        /// <param name="type">
        /// (Required.) One of the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// values that is to be examined.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the copilot reference generator
        /// <paramref name="type" /> falls within the defined value set;
        /// <see langword="false" /> otherwise.
        /// </returns>
        bool IsValid(CopilotReferenceGeneratorType type);
    }
}