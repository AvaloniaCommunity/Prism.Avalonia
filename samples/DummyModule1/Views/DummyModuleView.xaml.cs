using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Events;

namespace DummyModule.View
{
    public class DummyModuleView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        private TextBox _logTextBox;

        public DummyModuleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
