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

namespace BootstrapperShellSample
{
    class App : Application
    {
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().SetupWithoutStarting();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        static void Main(string[] args)
        {
            InitializeLogging();

            Bootstrapper bs = new Bootstrapper();

            bs.Run();
        }

//        public static void AttachDevTools(Window window)
//        {
//#if DEBUG
//            DevTools.Attach(window);
//#endif
//        }

        private static void InitializeLogging()
        {
#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }
    }
}
