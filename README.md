# Prism.Avalonia

[Prism.Avalonia](https://github.com/AvaloniaCommunity/Prism.Avalonia) provides your [Avalonia](https://avaloniaui.net/) apps with [Prism framework](https://github.com/PrismLibrary/Prism) support so you can navigate and perform dependency injection easier than before.

| Package | NuGet |
|-|-|
| Prism.Avalonia | [![Prism.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.Avalonia?dWidth=70&includePreReleases=true)](https://www.nuget.org/packages/Prism.Avalonia/)
| Prism.DryIoc.Avalonia | [![Prism.DryIoc.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.DryIoc.Avalonia?dWidth=70&includePreReleases=true)](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/)

Prism.Avalonia's logic and development approach is similar to that of [Prism for WPF](https://github.com/PrismLibrary/Prism/) so you can get started right away with Prism for Avalonia!

## Install

Add the DryIoc package to your project:

```powershell
Install-Package Prism.DryIoc.Avalonia -Version 8.1.97.2
```

## How to use

### App.xaml.cs

```csharp
public class App : PrismApplication
{
    public static bool IsSingleViewLifetime =>
        Environment.GetCommandLineArgs()
            .Any(a => a == "--fbdev" || a == "--drm");

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();              // <-- Required
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Register Services
        containerRegistry.Register<IRestService, RestService>();

        // Views - Generic
        containerRegistry.Register<MainWindow>();

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<SidebarView, SidebarViewModel>();
    }

    protected override IAvaloniaObject CreateShell()
    {
        if (IsSingleViewLifetime)
            return Container.Resolve<MainControl>(); // For Linux Framebuffer or DRM
        else
            return Container.Resolve<MainWindow>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        // Register modules
        moduleCatalog.AddModule<Module1.Module>();
        moduleCatalog.AddModule<Module2.Module>();
        moduleCatalog.AddModule<Module3.Module>();
    }

    /// <summary>Called after <seealso cref="Initialize"/>.</summary>
    protected override void OnInitialized()
    {
      // Register initial Views to Region.
      var regionManager = Container.Resolve<IRegionManager>();
      regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
      regionManager.RegisterViewWithRegion(RegionNames.SidebarRegion, typeof(SidebarView));
    }
}
```

### Program.cs

```csharp
public static class Program
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .With(new X11PlatformOptions
            {
                EnableMultiTouch = true,
                UseDBusMenu = true
            })
            .With(new Win32PlatformOptions
            {
                EnableMultitouch = true,
                AllowEglInitialization = true
            })
            .UseSkia()
            .UseReactiveUI()
            .UseManagedSystemDialogs();

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
        InitializeLogging();
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
        { IsBackground = true }.Start();
    }
}
```
