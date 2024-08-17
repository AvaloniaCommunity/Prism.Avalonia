using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using System.Threading;
using Prism.Ioc;
using BootstrapperShellSample.Views;

namespace BootstrapperShellSample
{
    class App : PrismApplication
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder
                .Configure<App>()
                .UsePlatformDetect();
#if DEBUG
            builder.LogToTrace();
#endif
            return builder;
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Application entry point. Avalonia is completely initialized.
        static void AppMain(Application app, string[] args)
        {
            // A cancellation token source that will be used to stop the main loop
            var cts = new CancellationTokenSource();

            // Start the main loop
            app.Run(cts.Token);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override AvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
