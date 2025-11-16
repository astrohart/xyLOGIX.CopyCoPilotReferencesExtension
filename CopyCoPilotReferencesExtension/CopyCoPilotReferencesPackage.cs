using Microsoft.VisualStudio.Shell;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace CopyCoPilotReferencesExtension
{
    /// <summary>
    /// Represents the Visual Studio package for the <c>CopyCoPilotReferences</c>
    /// extension.
    /// </summary>
    /// <remarks>
    /// This package is registered with Visual Studio to provide the functionality of
    /// the
    /// <c>CopyCoPilotReferences</c> extension.
    /// <para />
    /// It supports asynchronous initialization and background loading to improve
    /// performance during Visual Studio startup.
    /// </remarks>
    [PackageRegistration(
         UseManagedResourcesOnly = true, AllowsBackgroundLoading = true
     ), Guid(PackageGuidString), ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class CopyCoPilotReferencesPackage : AsyncPackage
    {
        /// <summary>
        /// The <see cref="T:System.Guid" /> string for the package.
        /// </summary>
        public const string PackageGuidString =
            "f5e53b6a-4f5f-4f87-b6d9-167b162f3b8b";

        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage" />
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
        static CopyCoPilotReferencesPackage() { }

        /// <summary>
        /// Creates a new instance of
        /// <see cref="T:CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage" />
        /// and returns a reference to it.
        /// </summary>
        /// <remarks>
        /// We've decorated this constructor with the
        /// <c>[Log(AttributeExclude = true)]</c> attribute in order to simplify the
        /// logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        public CopyCoPilotReferencesPackage() { }

        /// <summary>
        /// The async initialization portion of the package initialization process. This
        /// method is invoked from a background thread.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token to monitor for
        /// initialization cancellation, which can occur when VS is shutting down.
        /// </param>
        /// <param name="progress">
        /// (Required.) Reference to an instance of an object that implements the
        /// <see cref="T:System.IProgress`1" /> interface that iṡ provided with a reference
        /// to <see cref="T:Microsoft.VisualStudio.Shell.ServiceProgressData" /> as its
        /// type parameter.
        /// <para />
        /// This object is used to report progress during initialization.
        /// </param>
        /// <returns>
        /// A task representing the async work of package initialization, or an
        /// already completed task if there is none. Do not return null from this method.
        /// </returns>
        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress
        )
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(
                cancellationToken
            );
            await CopyCoPilotReferencesCommand.InitializeAsync(this);
        }
    }
}