using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Ioc;
using SampleMvvmApp.Services;

namespace SampleMvvmApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            // Initialize the WindowNotificationManager with the MainWindow
            var notifyService = ContainerLocator.Current.Resolve<INotificationService>();
            notifyService.SetHostWindow(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
