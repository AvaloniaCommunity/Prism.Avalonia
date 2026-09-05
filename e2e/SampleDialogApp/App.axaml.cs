using System;
using Avalonia;
using Prism.DryIoc;
using Prism.Ioc;
using SampleDialogApp.ViewModels;
using SampleDialogApp.Views;

namespace SampleDialogApp;

public partial class App : PrismApplication
{
    /*
    public override void Initialize()
    {
        // Required when overriding Initialize()
        base.Initialize();
#if DEBUG
        // Replaces the old this.AttachDevTools();
        // NOTE: This requires connection to, http://127.0.0.1:29414/ and some IT firewalls may block it.
        // Reference: https://docs.avaloniaui.net/tools/developer-tools/attaching-to-the-remote-tool
        this.AttachDeveloperTools();
        ////{
        ////    // Change the initialization key gesture (Default is F12)
        ////    options.Gesture = Avalonia.Input.KeyGesture.Parse("F11");
        ////});
#endif
    }
    */

    protected override AvaloniaObject CreateShell()
    {
        Console.WriteLine("CreateShell()");
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<MainWindow>();
        containerRegistry.RegisterDialog<MessageBoxView, MessageBoxViewModel>();
        containerRegistry.RegisterDialog<DialogView, DialogViewModel>();
        containerRegistry.RegisterDialogWindow<CustomDialogWindow>(nameof(CustomDialogWindow));
    }
}
