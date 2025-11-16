using CCPRE.Generators.References.Interfaces;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Diagnostics;
using xyLOGIX.Core.Debug;

namespace CCPRE.Generators.References.Factories
{
    /// <summary>
    /// Provides access to the one and only instance of the object that implements the
    /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
    /// interface.
    /// </summary>
    public static class GetFileCopilotReferenceGenerator
    {
        /// <summary>
        /// Initializes static data or performs actions that need to be performed once only
        /// for the
        /// <see
        ///     cref="T:CCPRE.Generators.References.Factories.GetFileCopilotReferenceGenerator" />
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any static members are referenced.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static GetFileCopilotReferenceGenerator() { }

        /// <summary>
        /// Obtains access to the sole instance of the object that implements the
        /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// interface, and returns a reference to it.
        /// </summary>
        /// <returns>
        /// Reference to the one, and only, instance of the object that implements the
        /// <see cref="T:CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator" />
        /// interface.
        /// </returns>
        [DebuggerStepThrough]
        [return: NotLogged]
        public static ICopilotReferenceGenerator SoleInstance()
        {
            ICopilotReferenceGenerator result;

            try
            {
                result = FileCopilotReferenceGenerator.Instance;
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