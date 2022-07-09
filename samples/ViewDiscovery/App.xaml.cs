using Avalonia;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using ViewDiscovery.Views;

namespace ViewDiscovery
{
    public partial class App : PrismApplication
    {
        /// <summary>App entry point.</summary>
        public App()
        {
        }

        // Prism v8.1 has WPF's `PrismApplicationBase.Initialize()` as a protected virtual void, not public. Should we too?
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Wire-up services and navigation Views here.
        }

        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
