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
