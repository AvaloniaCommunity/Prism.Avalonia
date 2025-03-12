using System;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using DryIoc;
using Prism.Container.DryIoc;
using Prism.Ioc;
using Prism.Navigation.Regions;
using ExceptionExtensions = System.ExceptionExtensions;

namespace Prism.DryIoc
{
    /// <summary>
    /// Base application class that uses <see cref="DryIocContainerExtension"/> as it's container.
    /// </summary>
    public abstract class PrismApplication : PrismApplicationBase
    {
        /// <summary>
        /// Create <see cref="Rules" /> to alter behavior of <see cref="IContainer" />
        /// </summary>
        /// <returns>An instance of <see cref="Rules" /></returns>
        protected virtual Rules CreateContainerRules() => DryIocContainerExtension.DefaultRules;

        /// <summary>
        /// Create a new <see cref="DryIocContainerExtension"/> used by Prism.
        /// </summary>
        /// <returns>A new <see cref="DryIocContainerExtension"/>.</returns>
        protected override IContainerExtension CreateContainerExtension()
        {
            return new DryIocContainerExtension(CreateContainerRules());
        }

        /// <summary>
        /// Registers the <see cref="Type"/>s of the Exceptions that are not considered 
        /// root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected override void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ContainerException));
        }

        /// <summary>Transition to new Main Window.</summary>
        /// <typeparam name="T">New shell to swap with.</typeparam>
        /// <param name="preUpdateAction"></param>
        //// protected virtual async Task TransitionMainWindow<T>(Action preUpdateAction)
        public async Task TransitionWindowAsync<T>(Func<Task> preUpdateAction = null)
        {
            // Grab current window (splash screen) to close it later.
            var currentShell = MainWindow;

            // Inform current window we're about to transition
            await preUpdateAction();

            // ---------------------------
            // Resolve the new window to transition to
            // Similar to `Initialize()`, below is a candidate for a Prism method
            // to handle the switch-over on Desktop.
            var newShell = (AvaloniaObject)Container.Resolve(typeof(T));

            if (newShell is not null)
            {
                //// Prism.Common.MvvmHelpers.AutowireViewModel(newShell);
                // AutoWireViewModel is 'internal static' so we can't access it here
                // The following is the same as, "MvvmHelpers.AutowireViewModel(newShell);"
                if (newShell is Control view &&
                    view.DataContext is null &&
                    Prism.Mvvm.ViewModelLocator.GetAutoWireViewModel(view) is null)
                {
                    Prism.Mvvm.ViewModelLocator.SetAutoWireViewModel(view, true);
                }

                RegionManager.SetRegionManager(newShell, Container.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();

                // Set the new Shell window in Prism to MainWindow
                InitializeShell(newShell);
            }

            // Show (new) main window
            OnInitialized();

            // WARNING:
            // This MUST be performed after showing the new window.
            // Otherwise, your program will exit.
            (currentShell as Window)?.Close();
        }
    }
}
