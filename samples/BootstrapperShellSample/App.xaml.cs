using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Logging.Serilog;
using Avalonia.Themes.Default;
using Avalonia.Markup.Xaml;
using CommonServiceLocator;
using DryIoc;
using Serilog;
using Prism.DryIoc;
using System.Threading;
using Prism.Ioc;
using BootstrapperShellSample.Views;

namespace BootstrapperShellSample
{
    class App : PrismApplication
    {
        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            InitializeLogging();
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

        private static void InitializeLogging()
        {
#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
