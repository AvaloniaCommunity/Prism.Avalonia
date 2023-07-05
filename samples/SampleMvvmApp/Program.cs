using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Logging;

namespace SampleMvvmApp;

public class Program
{
    public static AppBuilder BuildAvaloniaApp()
    {
        var builder = AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true, })
            .With(new Win32PlatformOptions
            {
                // EnableMultitouch = true,       // In Avalonia v11 this is always enabled
                // AllowEglInitialization = true, // Removed in Avalonia v11.0.0
            })
            .UseSkia();
            // .UseReactiveUI();

#if DEBUG
        builder.LogToTrace(LogEventLevel.Debug, LogArea.Property, LogArea.Layout, LogArea.Binding);
#endif
        return builder;
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [ExcludeFromCodeCoverage]
    static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
}
