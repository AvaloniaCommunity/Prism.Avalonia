using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis;
using Prism.DryIoc;
using Prism.Ioc;
using ViewDiscovery.Views;

namespace ViewDiscovery;

public partial class App : PrismApplication
{
    // Prism v8.1 has WPF's `PrismApplicationBase.Initialize()` as a protected virtual void, not public. Should we too?
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
    }

    /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
    /// <returns>Startup View.</returns>
    protected override IAvaloniaObject CreateShell()
    {
        System.Diagnostics.Debug.WriteLine("CreateShell()");
        return Container.Resolve<MainWindow>();
    }

    protected override void OnInitialized()
    {
        System.Diagnostics.Debug.WriteLine("OnInitialized()");
    }
}
