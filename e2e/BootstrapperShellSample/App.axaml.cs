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

#if DEBUG
            // Replaces the old this.AttachDevTools();
            // NOTE: This requires connection to, http://127.0.0.1:29414/ and some IT firewalls may block it.
            // Reference: https://docs.avaloniaui.net/tools/developer-tools/attaching-to-the-remote-tool
            this.AttachDeveloperTools();
            ////{
            ////    // Change the initialization key gesture (Default is F12)
            ////    options.Gesture = Avalonia.Input.KeyGesture.Parse("F11");
            ////});
#endif
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
