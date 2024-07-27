using Avalonia.Controls;
using Avalonia.Threading;
using ModulesSample.Infrastructure;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;
using Prism.Navigation.Regions;

namespace DummyModule2.View
{
    public partial class DummyModuleView2 : UserControl
    {
        private readonly IEventAggregator _eventAggregator;
        private TextBox _regionViewTextBox;

        public DummyModuleView2()
        {
        }

        public DummyModuleView2(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;

            InitializeComponent();

            _regionViewTextBox = this.FindControl<TextBox>("RegionViewTextBox");
            eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    regionManager.AddToRegion(
                        RegionNames.ListRegion,
                        new TextBlock { Text = "EventAggregator DummyEvent triggered for DummyModule2" });

                    //_regionViewTextBox.Text += "\n EventAggregator DummyEvent triggered for DummyModule2 \r\n";
                });
            });
        }
    }
}
