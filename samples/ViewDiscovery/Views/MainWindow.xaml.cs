using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Prism.Regions;

namespace ViewDiscovery.Views
{
    public class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            this.InitializeComponent();
            this.AttachDevTools();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewB));
            Test();
        }

        private async void Test()
        {
            var region = _regionManager.Regions["ContentRegion"];
            var viewA = region.Views.FirstOrDefault();
            var viewB = region.Views.Skip(1).FirstOrDefault();

            while (true)
            {
                await Task.Delay(2000);
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    region.Activate(viewB);
                });

                await Task.Delay(2000);
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    region.Activate(viewA);
                });
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
