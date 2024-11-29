using Avalonia;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using ViewDiscovery.Views;

namespace ViewDiscovery;

public class App : PrismApplication
{
    public App()
    {
        System.Diagnostics.Debug.WriteLine("App.Constructor()");
    }

    public override void Initialize()
    {
        System.Diagnostics.Debug.WriteLine("Initialize()");
        AvaloniaXamlLoader.Load(this); // Only required when overriding constructor
        base.Initialize();             // Must include when overriding
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        System.Diagnostics.Debug.WriteLine("RegisterTypes(...)");
        // Wire-up services and navigation Views here.
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

        // Alternative Approach:
        //  Instead of registering the Views in the MainWindow (as this samples does),
        //  you can register them here in OnInitialized(), or CreateShell(), or RegisterTypes(..)
        //  by resolving the `IRegionManager` from the container.
        //
        ////var regionManager = Container.Resolve<IRegionManager>();
        ////regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewA));
        ////regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewB));
    }
}
