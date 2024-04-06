using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;

////using Prism.Logging;

namespace ModulesSample
{
    public partial class MainWindow : Window
    {
        ////private readonly CallbackLogger logger;
        private readonly IEventAggregator eventAggregator;

        private TextBox _logTextBox;
        private ListBox _itemsControl;

        public MainWindow()
        {
        }

        public MainWindow(IEventAggregator eventAggregator)
        ////public MainWindow(CallbackLogger logger, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.AttachDevTools();

            _logTextBox = this.FindControl<TextBox>("LogTextBox");
            _itemsControl = this.FindControl<ListBox>("ItemsControl1");

            ////this.logger = logger;
            ////this.logger.Callback = this.Log;
            ////this.logger.ReplaySavedLogs();

            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _logTextBox.Text += "EventAggregator DummyEvent triggered \r\n";
                });
            });

            DataContext = this;
        }

        ////private void Log(string message, Category category, Priority priority)
        ////{
        ////    this.logTextBox.Text += string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2} \r\n", category,
        ////        priority, message);
        ////}
    }
}
