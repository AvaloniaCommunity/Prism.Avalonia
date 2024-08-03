using Avalonia;
using Avalonia.Markup.Xaml;
using ModulesSample.Module_System_Logic;
using Prism.Avalonia.Infrastructure;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Avalonia.LinuxFramebuffer;
using System.Linq;
using System;
using System.Globalization;
using System.Threading;
using Avalonia.Dialogs;
using ModulesSample.Infrastructure;

namespace ModulesSample
{
    public class App : PrismApplication
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new X11PlatformOptions
                {
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                })
                .With(new Win32PlatformOptions())
                .UseSkia()
                .UseManagedSystemDialogs();

#if DEBUG
            builder.LogToTrace();
#endif
            return builder;
        }

        public static bool IsSingleViewLifetime =>
            Environment.GetCommandLineArgs()
                .Any(a => a == "--fbdev" || a == "--drm");

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static int Main(string[] args)
        {
            double GetScaling()
            {
                var idx = Array.IndexOf(args, "--scaling");
                if (idx != 0 && args.Length > idx + 1 &&
                    double.TryParse(args[idx + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out var scaling))
                    return scaling;

                return 1;
            }

            var builder = BuildAvaloniaApp();
            if (args.Contains("--fbdev"))
            {
                SilenceConsole();
                return builder.StartLinuxFbDev(args, scaling: GetScaling());
            }
            else if (args.Contains("--drm"))
            {
                SilenceConsole();
                return builder.StartLinuxDrm(args, scaling: GetScaling());
            }
            else
                return builder.StartWithClassicDesktopLifetime(args);
        }

        static void SilenceConsole()
        {
            new Thread(() =>
            {
                Console.CursorVisible = false;
                while (true)
                    Console.ReadKey(true);
            })
            {
                IsBackground = true,
            }.Start();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IModuleTracker, ModuleTracker>();
        }

        protected override AvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DummyModule.DummyModule1>();
            moduleCatalog.AddModule<DummyModule2.DummyModule2>();

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
