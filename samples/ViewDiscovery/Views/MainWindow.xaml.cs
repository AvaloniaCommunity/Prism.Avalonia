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

        public MainWindow() { }

        public MainWindow(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            this.InitializeComponent();
            this.AttachDevTools();

            // This is the wrong approach
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewA));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ViewB));
            Test();
        }

        private async void Test()
        {
            //// await Task.Delay(2000);

            if (!_regionManager.Regions.ContainsRegionWithName(RegionNames.ContentRegion))
            {
                System.Diagnostics.Debugger.Break();
                return;
            }

            var region = _regionManager.Regions["ContentRegion"];

            //// var viewA = container.Resolve<ViewA>();
            //// region.Add(viewA, nameof(ViewA));
            //// region.Activate(viewA);

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
