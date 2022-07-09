using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicApp.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //// AttachDevTools();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
