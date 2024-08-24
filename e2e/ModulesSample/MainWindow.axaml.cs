using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;

namespace ModulesSample
{
    public partial class MainWindow : Window
    {
        private readonly IEventAggregator _eventAggregator;

        private TextBox _logTextBox;
        private ListBox _itemsControl;
        private int _counter = 0;

        public MainWindow()
        {
        }

        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            _logTextBox = this.FindControl<TextBox>("LogTextBox");
            _itemsControl = this.FindControl<ListBox>("ItemsControl1");

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _counter++;
                    if (_counter % 5 == 0)
                        _logTextBox.Text = string.Empty;

                    _logTextBox.Text += $"EventAggregator DummyEvent triggered: {_counter}\r\n";
                });
            });

            DataContext = this;
        }
    }
}
