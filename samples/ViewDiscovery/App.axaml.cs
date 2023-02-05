using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using ViewDiscovery.ViewModels;
using ViewDiscovery.Views;

namespace ViewDiscovery;

public class App : PrismApplication
{
    public App()
    {
        Console.WriteLine("Constructor()");
    }

    public override void Initialize()
    {
        System.Diagnostics.Debug.WriteLine("Initialize()");
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Wire-up services and navigation Views here.
        System.Diagnostics.Debug.WriteLine("RegisterTypes()");
        containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        containerRegistry.RegisterForNavigation<ViewB, ViewBViewModel>();
    }

    /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
    /// <returns>Startup View.</returns>
    protected override AvaloniaObject CreateShell()
    {
        System.Diagnostics.Debug.WriteLine("CreateShell()");
        return Container.Resolve<MainWindow>();
    }

    protected override void OnInitialized()
    {
        System.Diagnostics.Debug.WriteLine("OnInitialized()");

        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewA));
        regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewB));
    }
}
