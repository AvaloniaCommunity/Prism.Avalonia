// Temp disabled logging:
// - Prism.Logging has been moved and `ILoggerFacade` is deprecated.Prism.Logging.Serilog is out of date.
// - https://github.com/augustoproiete/prism-logging-serilog/issues/3
using System.Threading;
using DummyModule.View;
using ModulesSample.Infrastructure;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;
using Prism.Ioc;
////using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace DummyModule
{
    public class DummyModule1 : IModule
    {
        ////private readonly ILoggerFacade _logger;
        private readonly IModuleTracker _moduleTracker;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public DummyModule1(IModuleTracker moduleTracker, IEventAggregator eventAggregator, IRegionManager regionManager)
        ////public DummyModule1(ILoggerFacade logger, IModuleTracker moduleTracker, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            ////_logger = logger;
            _moduleTracker = moduleTracker;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        private void DummyEventPublisher()
        {
            while (true)
            {
                Thread.Sleep(2500);
                _eventAggregator.GetEvent<DummyEvent>().Publish();
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Thread thread = new Thread(new ThreadStart(DummyEventPublisher));
            thread.Start();

            _regionManager.RegisterViewWithRegion(RegionNames.Region1, typeof(DummyModuleView));
            _moduleTracker.RecordModuleInitialized(ModuleNames.ModuleDummy1);
        }
    }
}
