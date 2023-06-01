using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SampleMvvmApp.Services;
using SampleMvvmApp.ViewModels;
using SampleMvvmApp.Views;

namespace SampleMvvmApp;

/// <summary>
///   Application entry point.
///
///   The methods below are laid out in their order of operation to assist
///   you getting started with Prism.Avalonia.
/// </summary>
public class App : PrismApplication
{
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

        // Initializes Prism.Avalonia - DO NOT REMOVE
        base.Initialize();
    }

    /// <summary>Register Services and Views.</summary>
    /// <param name="containerRegistry"></param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        Console.WriteLine("RegisterTypes()");

        // Services
        containerRegistry.RegisterSingleton<INotificationService, NotificationService>();

        // Views - Generic
        //// containerRegistry.Register<SidebarView>();  // Not required
        //// containerRegistry.Register<MainWindow>();

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<SubSettingsView, SubSettingsViewModel>();
    }

    /// <summary>Register optional modules in the catalog.</summary>
    /// <param name="moduleCatalog">Module Catalog.</param>
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
    /// <returns>Startup View.</returns>
    protected override AvaloniaObject CreateShell()
    {
        Console.WriteLine("CreateShell()");
        return Container.Resolve<MainWindow>();
    }

    /// <summary>Called after Initialize.</summary>
    protected override void OnInitialized()
    {
        // Register Views to the Region it will appear in. Don't register them in the ViewModel.
        var regionManager = Container.Resolve<IRegionManager>();

        // WARNING: Prism v11.0.0-prev4
        // - DataTemplates MUST define a DataType or else an XAML error will be thrown
        // - Error: DataTemplate inside of DataTemplates must have a DataType set
        regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
        regionManager.RegisterViewWithRegion(RegionNames.SidebarRegion, typeof(SidebarView));

        ////var logService = Container.Resolve<ILogService>();
        ////logService.Configure("swlog.config");
    }
}
