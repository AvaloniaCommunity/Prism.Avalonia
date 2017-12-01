using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;

namespace DummyModule2.View
{
    public class DummyModuleView2 : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        private TextBox regionViewTextBox;

        public DummyModuleView2(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.InitializeComponent();

            regionViewTextBox = this.FindControl<TextBox>("RegionViewTextBox");
            eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    regionViewTextBox.Text += "\n EventAggregator DummyEvent triggered for DummyModule2 \r\n";
                });
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
