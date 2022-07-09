using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using BasicApp.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace BasicApp
{
    /// <summary>
    ///   Application entry point.
    ///   The methods in this file are layed out in their respective calling order
    ///   to help you learn the order of operation.
    /// </summary>
    public class App : PrismApplication
    {
        private static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder
                .Configure<App>()
                .UsePlatformDetect();
            // from personal project
            ////.With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true })
            ////.With(new Win32PlatformOptions { EnableMultitouch = true, AllowEglInitialization = true })
            ////.UseSkia()
            ////.UseReactiveUI();

#if DEBUG
            builder.LogToTrace();
#endif
            return builder;
        }

        /// <summary>App entry point.</summary>
        public App()
        {
            Console.WriteLine("Constructor()");
        }

        // Note:
        //  Though, Prism.WPF v8.1 uses, `protected virtual void Initialize()`
        //  Avalonia's AppBuilderBase.cs calls, `.Setup() { ... Instance.Initialize(); ... }`
        //  Therefore, we need this as a `public override void` in PrismApplicationBase.cs
        public override void Initialize()
        {
            Console.WriteLine("Initialize()");
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Console.WriteLine("RegisterTypes()");
        }


        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override IAvaloniaObject CreateShell()
        {
            Console.WriteLine("CreateShell()");
            return Container.Resolve<MainWindow>();
        }
    }
}
