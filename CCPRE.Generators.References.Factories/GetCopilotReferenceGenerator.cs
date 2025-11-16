using CCPRE.Generators.References.Constants;
using CCPRE.Generators.References.Interfaces;
using CCPRE.Generators.References.Validators.Factories;
using CCPRE.Generators.References.Validators.Interfaces;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Debug;

namespace CCPRE.Generators.References.Factories
{
    /// <summary>
    /// Provides factory method(s) for obtaining
    /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
    /// instance(s) based on the generator type.
    /// </summary>
    /// <remarks>
    /// This <see langword="static" /> class implements the Strategy Factory
    /// pattern for creating Copilot reference generator(s).
    /// </remarks>
    public static class GetCopilotReferenceGenerator
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator" />
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
        static GetCopilotReferenceGenerator() { }

        /// <summary>
        /// Gets a reference to an instance of an object that implements the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator" />
        /// interface.
        /// </summary>
        private static ICopilotReferenceGeneratorTypeValidator
            CopilotReferenceGeneratorTypeValidator
        {
            [DebuggerStepThrough] get;
        } = GetCopilotReferenceGeneratorTypeValidator.SoleInstance();

        /// <summary>
        /// Obtains a reference to the sole instance of the
        /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// that corresponds to the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">
        /// (Required.) A
        /// <see
        ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
        /// value that specifies which generator to retrieve.
        /// </param>
        /// <returns>
        /// Reference to an instance of an object that implements the
        /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// interface, or <see langword="null" /> if no generator is available for
        /// the specified <paramref name="type" />.
        /// </returns>
        /// <remarks>
        /// This method returns <see langword="null" /> if <paramref name="type" />
        /// is
        /// <see
        ///     cref="F:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown" />
        /// .
        /// <para />
        /// This method returns <see langword="null" /> if <paramref name="type" />
        /// has an unrecognized value.
        /// </remarks>
        [return: NotLogged]
        public static ICopilotReferenceGenerator OfType(
            CopilotReferenceGeneratorType type
        )
        {
            ICopilotReferenceGenerator result = null;

            try
            {
                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"GetCopilotReferenceGenerator.OfType: Checking whether the Copilot reference generator type, '{type}', is within the defined value set..."
                );

                // Check whether the Copilot reference generator type, 'type', is within the defined
                // value set.  If this is not the case, then write an error message
                // to the log file, and then terminate the execution of this method,
                // while returning the default return value.
                if (!CopilotReferenceGeneratorTypeValidator.IsValid(type))
                {
                    // The Copilot reference generator type, 'type', is NOT within the defined value set.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"GetCopilotReferenceGenerator.OfType: *** ERROR *** The Copilot reference generator type, '{type}', is NOT within the defined value set.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"*** ERROR *** FAILED to obtain a reference to the Copilot Reference Generator for the entity of type, '{type}'.  Stopping..."
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"GetCopilotReferenceGenerator.OfType: *** SUCCESS *** The Copilot reference generator type, '{type}', is within the defined value set.  Proceeding..."
                );

                switch (type)
                {
                    case CopilotReferenceGeneratorType.File:
                        result = GetFileCopilotReferenceGenerator
                            .SoleInstance();
                        break;

                    // Additional cases will be added here as more strategies are implemented

                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(type), type,
                            $"Unknown Copilot reference generator type: '{type}'"
                        );
                }
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = null;
            }

            DebugUtils.WriteLine(
                result != null ? DebugLevel.Info : DebugLevel.Error,
                result != null
                    ? $"*** SUCCESS *** Obtained a reference to the Copilot Reference Generator for the entity of type, '{type}'.  Proceeding..."
                    : $"*** ERROR *** FAILED to obtain a reference to the Copilot Reference Generator for the entity of type, '{type}'.  Stopping..."
            );

            return result;
        }
    }
}