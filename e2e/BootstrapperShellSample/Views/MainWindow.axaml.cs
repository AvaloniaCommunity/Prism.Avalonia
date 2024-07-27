using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BootstrapperShellSample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();
        }
    }
}
