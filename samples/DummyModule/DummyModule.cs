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
    public class DummyModule : IModule
    {
        ////private readonly ILoggerFacade logger;
        private readonly IModuleTracker moduleTracker;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public DummyModule(IModuleTracker moduleTracker, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            // (ILoggerFacade logger,
            ////this.logger = logger;
            this.moduleTracker = moduleTracker;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }

        private void DummyEventPublisher()
        {
            while (true)
            {
                Thread.Sleep(2500);
                eventAggregator.GetEvent<DummyEvent>().Publish();
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Thread thread = new Thread(new ThreadStart(DummyEventPublisher));
            thread.Start();

            regionManager.RegisterViewWithRegion("Region1", typeof(DummyModuleView));

            this.moduleTracker.RecordModuleInitialized(KnownModuleNames.ModuleDummy);
        }
    }
}
