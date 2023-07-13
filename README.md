# Prism.Avalonia

[Prism.Avalonia](https://github.com/AvaloniaCommunity/Prism.Avalonia) provides your [Avalonia](https://avaloniaui.net/) apps with [Prism framework](https://github.com/PrismLibrary/Prism) support so you can **Navigate**, create **Dialog Windows** and **Notifications**, provide **Dependency Injection** and internal **Messaging** easier than before!  You will need both packages installed to get started.

![Sample Outlookish](logo/Sample-Outlookish.png)

With Prism.Avalonia's logic and development approach being **similar** to that of [Prism for WPF](https://github.com/PrismLibrary/Prism/), you can get started right away! Keep in mind, they are **similar** and not 1-to-1. Check out our [Wiki](https://github.com/AvaloniaCommunity/Prism.Avalonia/wiki) and [Avalonia Outlookish](https://github.com/DamianSuess/Learn.PrismAvaloniaOutlookish) app for tips and tricks.

| Package | Stable | Preview
|-|-|-|
| Prism.Avalonia | [![Prism.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.Avalonia?dWidth=70&includePreReleases=false)](https://www.nuget.org/packages/Prism.Avalonia/) | [![Prism.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.Avalonia?dWidth=70&includePreReleases=true)](https://www.nuget.org/packages/Prism.Avalonia/)
| Prism.DryIoc.Avalonia | [![Prism.DryIoc.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.DryIoc.Avalonia?dWidth=70&includePreReleases=false)](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/) | [![Prism.DryIoc.Avalonia NuGet Badge](https://buildstats.info/nuget/Prism.DryIoc.Avalonia?dWidth=70&includePreReleases=true)](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/)

## Version Notice

Choose the NuGet package version that matches your Avalonia version. Just like Prism.WPF or Prism.Maui, your project must reference both the Prism.Avalonia (_Core_) and Prism.DryIoc.Avalonia (_IoC container_) packages to work.

| Avalonia Version | NuGet Package |
|-|-|
| **11.0**        | 8.1.97.11000 ([Core](https://www.nuget.org/packages/Prism.Avalonia/8.1.97.11000)) ([DryIoc](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/8.1.97.11000))
| **0.10.21**     | 8.1.97.1021 ([Core](https://www.nuget.org/packages/Prism.Avalonia/8.1.97.1021)) ([DryIoc](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/8.1.97.1021))
| **11.0 RC-1.1** | 8.1.97.11000-rc1.1 ([Core](https://www.nuget.org/packages/Prism.Avalonia/8.1.97.11000-rc1.1)) ([DryIoc](https://www.nuget.org/packages/Prism.DryIoc.Avalonia/8.1.97.11000-rc1.1))
| 11.0 Preview 8  | 8.1.97.11-preview.11.8
| 11.0 Preview 5  | 8.1.97.4-preview.11.5
| 11.0 Preview 4  | 8.1.97.3-preview.11.4

Be sure to check out the [ChangeLog.md](ChangeLog.md) and [Upgrading-to-Avalonia-11.md](Upgrading-to-Avalonia-11.md) when upgrading your NuGet packages. Also, view the official [Avalonia Upgrading from v0.10](https://docs.avaloniaui.net/docs/next/stay-up-to-date/upgrade-from-0.10).

## Install

Add the Prism.Avalonia and its DryIoc packages to your project:

```powershell
# Avalonia v0.10.1021
Install-Package Prism.Avalonia -Version 8.1.97.1021
Install-Package Prism.DryIoc.Avalonia -Version 8.1.97.1021

# Avalonia v11-preview8
Install-Package Prism.Avalonia -Version 8.1.97.11000
Install-Package Prism.DryIoc.Avalonia -Version 8.1.97.11000
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

    protected override AvaloniaObject CreateShell()
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

Your default Avalonia `Program.cs` file does not need modified. Below is provided as a sample.

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
            .With(new Win32PlatformOptions())
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

## Branching Strategy

Below is a basic branching hierarchy and strategy.

| Branch | Purpose
|-|-|
| `master`    | All releases are tagged published using the `master` branch
| `develop`   | The **default** & active development branch. When a feature set is completed and ready for public release, the `develop` branch will be merged into `master` and a new NuGet package will be published.
| `feature/*` | New feature branch. Once completed, it is merged into `develop` and the branch must be deleted.
| `stable/*`  | Stable release base build which shares cherry-picked merges from `develop`. This branch **must not** be deleted.

## Contributing

Prism.Avalonia is an open-source project under the MIT license. We encourage community members like yourself to contribute.


**Sponsored by:** [Suess Labs](https://suesslabs.com)
