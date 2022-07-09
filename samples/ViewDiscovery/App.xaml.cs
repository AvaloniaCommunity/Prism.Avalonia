using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Themes.Default;
using Avalonia.Markup.Xaml;
using Serilog;
using Prism.DryIoc;
using Prism.Ioc;
using ViewDiscovery.Views;
using System.Threading;

namespace ViewDiscovery
{
    public partial class App : PrismApplication
    {
        /// <summary>App entry point.</summary>
        public App()
        {
            //AvaloniaXamlLoader.Load(this);
            //base.Initialize();
        }

        // Prism v8.1 has WPF's `PrismApplicationBase.Initialize()` as a protected virtual void, not public. Should we too?
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
