using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SampleDialogApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        // When referencing Avalonia package, XamlNameReferenceGenerator
        // we may not need this method
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
