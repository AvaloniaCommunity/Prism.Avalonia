using Avalonia.Controls;
using Prism.Events;

namespace DummyModule.View
{
    public partial class DummyModuleView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        private TextBox _logTextBox;

        public DummyModuleView()
        {
            InitializeComponent();
        }
    }
}
