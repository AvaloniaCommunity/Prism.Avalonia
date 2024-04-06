using Avalonia;
using Avalonia.Controls;

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
        //  This code has moved to 'DashboardView.axaml.cs
        //
        //  In v0.10.x, users set the HostWindow inside the main shell Window passing 'this'
        //  As of v11, the initialization no longer works from MainWindow and must be defined
        //  in the UserControl by providing `notifyService.SetHostWindow(TopLevel.GetTopLevel(this))`
        //  in the view's .axaml.cs `override void OnAttachedToVisualTree(..)` method.
        //
        // OLD: Avalonia v0.10.18
        ////// Initialize the WindowNotificationManager with the MainWindow
        ////var notifyService = ContainerLocator.Current.Resolve<INotificationService>();
        ////notifyService.SetHostWindow(this);
    }
}
