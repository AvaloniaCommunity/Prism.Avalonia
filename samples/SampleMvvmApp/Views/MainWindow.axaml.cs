using Avalonia;
using Avalonia.Controls;
using Prism.Ioc;
using SampleMvvmApp.Services;

namespace SampleMvvmApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        // Avalonia v11-Preview 5 Breaking Change:
        //  Previously, users had to set the HostWindow inside the main shell Window
        //  as you see below. Now, users can define this from any UserControl view
        //  by simply providing `notifyService.SetHostWindow(TopLevel.GetTopLevel(this))`
        //  in the view's .axaml.cs `override void OnAttachedToVisualTree(..)` method.
        //
        // OLD: Avalonia v0.10.18
        ////// Initialize the WindowNotificationManager with the MainWindow
        ////var notifyService = ContainerLocator.Current.Resolve<INotificationService>();
        ////notifyService.SetHostWindow(this);
    }
}
