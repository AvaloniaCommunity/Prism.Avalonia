using Avalonia;

namespace BasicMvvmApp
{
    public class Program
    {
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true, })
                .With(new Win32PlatformOptions { EnableMultitouch = true, AllowEglInitialization = true, })
                .UseSkia();
                // .UseReactiveUI();

#if DEBUG
            builder.LogToTrace();
#endif
            return builder;
        }

        static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}
