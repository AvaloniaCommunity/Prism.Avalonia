# Prism.Avalonia

Prism (https://github.com/PrismLibrary/Prism) framework support for Avalonia UI.

This library actually copies functionality of Prism for WPF implementation, which can be found here:
https://github.com/PrismLibrary/Prism/tree/master/Source/Wpf
  
Logic and approach for development of your applications remained the same as it was for Prism.Wpf library. 

## Install

Add package from nuget to your project:

```powershell
Install-Package Prism.Avalonia -Version 7.2.0.1429
```

DryIoc:

```powershell
Install-Package Prism.DryIoc.Avalonia -Version 7.2.0.1429
```

Unity:

```powershell
Install-Package Prism.Unity.Avalonia -Version 7.2.0.1429
```

Autofac (_Deprecated from Prism 8.x_):

```powershell
Install-Package Prism.Autofac.Avalonia -Version 7.1.0.431
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
        base.Initialize();
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
        // TODO: Register modules
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
