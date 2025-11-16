using CCPRE.Generators.References.Constants;
using CCPRE.Generators.References.Validators.Interfaces;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Debug;

namespace CCPRE.Generators.References.Validators
{
    /// <summary>
    /// Validates whether certain value(s) are within the defined value set of the
    /// <see
    ///     cref="T:CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType" />
    /// enumeration.
    /// </summary>
    public class
        CopilotReferenceGeneratorTypeValidator :
        ICopilotReferenceGeneratorTypeValidator
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that need to be
        /// performed once only for the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any <see langword="static" /> members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// <para />
        /// This particular constructor is here to prohibit direct instantiation of this
        /// <c>Singleton</c> object.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static CopilotReferenceGeneratorTypeValidator() { }

        /// <summary>
        /// Empty, <see langword="private" /> constructor to prohibit direct
        /// allocation of this class.
        /// </summary>
        /// <remarks>
        /// Even though this constructor be marked <see langword="private" />, we
        /// can still perform initialization of this object here.
        /// </remarks>
        [Log(AttributeExclude = true)]
        private CopilotReferenceGeneratorTypeValidator() { }

        /// <summary>
        /// Gets a reference to the one and only instance of the object that implements the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator" />
        /// interface.
        /// </summary>
        public static ICopilotReferenceGeneratorTypeValidator Instance
        {
            [DebuggerStepThrough] get;
        } = new CopilotReferenceGeneratorTypeValidator();

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
        public bool IsValid(CopilotReferenceGeneratorType type)
        {
            var result = false;

            try
            {
                // Dump the argument of the parameter, 'type', to the log
                DebugUtils.WriteLine(
                    DebugLevel.Debug,
                    $"CopilotReferenceGeneratorTypeValidator.IsValid: type = '{type}'"
                );

                /*
                 * For cybersecurity reasons, and to defeat reverse-engineering,
                 * check the value of the 'type' parameter to ensure that it
                 * is not set to a value outside the set of valid values defined
                 * by the CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType
                 * enumeration.
                 *
                 * In principle, since all C# enums devolve to integer values, a
                 * hacker could insert a different value into the CPU register that the
                 * 'type' parameter is read from and thereby make this application
                 * do something it's not intended to do.
                 */

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopilotReferenceGeneratorTypeValidator.IsValid: Checking whether the value of the 'type' parameter, i.e., '{type}', is within the defined value set of its enumerated data type..."
                );

                // Check whether the value of the 'type' parameter is within the defined value set of its 
                // enumeration data type.  If this is not the case, then write an error message to the log
                // file, and then terminate the execution of this method while returning the default return
                // value.
                if (!Enum.IsDefined(
                        typeof(CopilotReferenceGeneratorType), type
                    ))
                {
                    // The value of the 'type' parameter is NOT within the defined value set for its enumerated data type.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        $"*** ERROR *** The value of the 'type' parameter, i.e., '{type}', is NOT within the defined value set of its enumerated data type.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"CopilotReferenceGeneratorTypeValidator.IsValid: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    $"CopilotReferenceGeneratorTypeValidator.IsValid: *** SUCCESS *** The value of the 'type' parameter, i.e., '{type}', is within the defined value set of its enumerated data type.  Proceeding..."
                );

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopilotReferenceGeneratorTypeValidator.IsValid: Checking whether the 'Unknown' value has NOT been specified for the 'type' parameter..."
                );

                // Check whether the 'Unknown' value has been specified for the 'type' parameter.  If this is the case, then
                // write an error message to the log file, and then terminate the execution of this method, returning the default
                // return value in order to indicate that this method failed.
                if (CopilotReferenceGeneratorType.Unknown.Equals(type))
                {
                    // The 'Unknown' value has been specified for the 'type' parameter.  This is not desirable.
                    DebugUtils.WriteLine(
                        DebugLevel.Error,
                        "*** ERROR *** The 'Unknown' value has been specified for the 'type' parameter.  Stopping..."
                    );

                    DebugUtils.WriteLine(
                        DebugLevel.Debug,
                        $"CopilotReferenceGeneratorTypeValidator.IsValid: Result = {result}"
                    );

                    // stop.
                    return result;
                }

                DebugUtils.WriteLine(
                    DebugLevel.Info,
                    "CopilotReferenceGeneratorTypeValidator.IsValid: *** SUCCESS *** The 'Unknown' value has NOT been specified for the 'type' parameter.  Proceeding..."
                );

                // TODO: Add any additional validation logic here

                /*
                 * If we made it here, then assume that the input data is valid.
                 */

                result = true;
            }
            catch (Exception ex)
            {
                // dump all the exception info to the log
                DebugUtils.LogException(ex);

                result = false;
            }

            return result;
        }
    }
}