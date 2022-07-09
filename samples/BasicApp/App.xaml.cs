using Avalonia;
using Avalonia.Markup.Xaml;
using BasicApp.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace BasicApp
{
    public partial class App : PrismApplication
    {
        /// <summary>App entry point.</summary>
        public App()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        // No longer override, Initialize(), use the constructor.
        ////public override void Initialize()
        ////{
        ////    AvaloniaXamlLoader.Load(this);
        ////    base.Initialize();
        ////}

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
