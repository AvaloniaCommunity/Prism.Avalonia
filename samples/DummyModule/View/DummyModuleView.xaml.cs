using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Events;

namespace DummyModule.View
{
    public class DummyModuleView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        private TextBox logTextBox;

        public DummyModuleView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
