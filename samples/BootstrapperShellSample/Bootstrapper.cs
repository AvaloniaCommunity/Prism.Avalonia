using Avalonia;
using Avalonia.Controls;
using BootstrapperShellSample.Views;
using DryIoc;
using Prism.DryIoc;

namespace BootstrapperShellSample
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
