using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Prism.Regions;
using Prism.Ioc;

namespace ViewDiscovery.Views
{
    public class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            Test();
        }

        // Issue Avalonia-v11-preview4:
        // In this example, ideally we want to use this constructor to register
        // the views with the Region. However, the 'ContentRegion' did not get registered yet.
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
            //// var regionManager = ContainerLocator.Current.Resolve<IRegionManager>();

            if (!_regionManager.Regions.ContainsRegionWithName(RegionNames.ContentRegion))
            {
                // ISSUE: Avalonia v11-prev4 can't find the region
                // With v0.10.x this does not happen.
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
