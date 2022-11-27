﻿using System.Threading;
using Avalonia;
using Avalonia.Controls;

namespace ViewDiscovery
{
    public class Program
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder
                .Configure<App>()
                .UsePlatformDetect();
            // from personal project
            ////.With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true })
            ////.With(new Win32PlatformOptions { AllowEglInitialization = true })
            ////.UseSkia()
            ////.UseReactiveUI()
            ////.LogToTrace();
#if DEBUG
            builder.LogToTrace();
#endif
            return builder;
        }

        static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            // From old v7.2 sample
            //BuildAvaloniaApp().Start(AppMain, args);
        }

        // Application entry point. Avalonia is completely initialized.
        static void AppMain(Application app, string[] args)
        {
            // A cancellation token source that will be used to stop the main loop
            var cts = new CancellationTokenSource();

            // Start the main loop
            app.Run(cts.Token);
        }
    }
}
