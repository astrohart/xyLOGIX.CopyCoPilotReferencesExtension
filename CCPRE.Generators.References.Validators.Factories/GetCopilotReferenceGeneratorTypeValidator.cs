using CCPRE.Generators.References.Validators.Interfaces;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Debug;

namespace CCPRE.Generators.References.Validators.Factories
{
    /// <summary>
    /// Provides access to the one and only instance of the object that implements the
    /// <see
    ///     cref="T:CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator" />
    /// interface.
    /// </summary>
    public static class GetCopilotReferenceGeneratorTypeValidator
    {
        /// <summary>
        /// Initializes static data or performs actions that need to be performed once only
        /// for the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.Factories.GetCopilotReferenceGeneratorTypeValidator" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any static members are referenced.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static GetCopilotReferenceGeneratorTypeValidator() { }

        /// <summary>
        /// Obtains access to the sole instance of the object that implements the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator" />
        /// interface, and returns a reference to it.
        /// </summary>
        /// <returns>
        /// Reference to the one, and only, instance of the object that implements the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator" />
        /// interface.
        /// </returns>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static ICopilotReferenceGeneratorTypeValidator SoleInstance()
        {
            ICopilotReferenceGeneratorTypeValidator result;

            try
            {
                result = CopilotReferenceGeneratorTypeValidator.Instance;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = default;
            }

            return result;
        }
    }
}