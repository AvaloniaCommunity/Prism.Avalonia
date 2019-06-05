using System.Threading;
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

        protected override IStyledProperty CreateShell()
        {
            AppBuilderInstance = App.BuildAvaloniaApp();

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            var mainWindow = base.Shell as MainWindow;
            mainWindow.Show();
            Application.Current.Run(mainWindow);
        }
    }
}
