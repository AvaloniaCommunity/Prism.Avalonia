using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Regions;

namespace ViewDiscovery.Views
{
    public class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            this.InitializeComponent();
            this.AttachDevTools();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
