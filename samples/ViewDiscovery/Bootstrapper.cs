using ViewDiscovery.Views;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using DryIoc;
using Prism.DryIoc;

namespace ViewDiscovery
{
    class Bootstrapper : DryIocBootstrapper
    {
        private AppBuilder AppBuilderInstance;

        protected override IAvaloniaObject CreateShell()
        {
            AppBuilderInstance = App.BuildAvaloniaApp();

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            AppBuilderInstance.Start(base.Shell as Window);
        }
    }
}
