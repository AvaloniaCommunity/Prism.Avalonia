using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;
using Prism.Regions;

namespace DummyModule2.View
{
    public class DummyModuleView2 : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        private TextBox regionViewTextBox;

        public DummyModuleView2(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;

            this.InitializeComponent();

            regionViewTextBox = this.FindControl<TextBox>("RegionViewTextBox");
            eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    regionManager.AddToRegion("ListRegion", new TextBlock { Text = "EventAggregator DummyEvent triggered for DummyModule2" });
                    //regionViewTextBox.Text += "\n EventAggregator DummyEvent triggered for DummyModule2 \r\n";
                });
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
