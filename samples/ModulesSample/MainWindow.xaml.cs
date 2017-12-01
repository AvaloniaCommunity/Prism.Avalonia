using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;
using Prism.Logging;

namespace ModulesSample
{
    public class MainWindow : Window
    {
        private readonly CallbackLogger logger;
        private readonly IEventAggregator eventAggregator;

        private TextBox logTextBox;
        private ListBox itemsControl;

        public MainWindow(CallbackLogger logger, IEventAggregator eventAggregator)
        {
            this.InitializeComponent();
            this.AttachDevTools();

            this.logTextBox = this.FindControl<TextBox>("LogTextBox");
            this.itemsControl = this.FindControl<ListBox>("ItemsControl1");

            this.logger = logger;
            this.logger.Callback = this.Log;
            this.logger.ReplaySavedLogs();

            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    logTextBox.Text += "EventAggregator DummyEvent triggered \r\n";
                });
            });

            this.DataContext = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Log(string message, Category category, Priority priority)
        {
            this.logTextBox.Text += string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2} \r\n", category,
                priority, message);
        }
    }
}
