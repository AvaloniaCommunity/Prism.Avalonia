using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SampleDialogApp
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
        ////private void InitializeComponent()
        ////{
        ////    AvaloniaXamlLoader.Load(this);
        ////}
    }
}
