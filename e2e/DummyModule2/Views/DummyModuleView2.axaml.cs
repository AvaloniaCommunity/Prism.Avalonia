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
        private readonly Label _regionViewTextBox;
        private int _counter = 0;

        public DummyModuleView2()
        {
            // Placed here so Avalonia viewer doesn't freak out
            // Since we're not using an MVVM approach here
        }

        public DummyModuleView2(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;

            InitializeComponent();

            _regionViewTextBox = this.FindControl<Label>("RegionViewTextBox");

            // Note: 'keepSubscriberReferenceAlive' is set to true to avoid the WeakReferene
            eventAggregator.GetEvent<DummyEvent>().Subscribe(() =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _counter++;

                    regionManager.AddToRegion(
                        RegionNames.ListRegion,
                        new TextBlock { Text = $"EventAggregator DummyEvent triggered for DummyModule2: {_counter}" });

                    _regionViewTextBox.Content = $"EventAggregator DummyEvent triggered for DummyModule2: {_counter}";
                });
            },
            keepSubscriberReferenceAlive: true);
        }
    }
}
