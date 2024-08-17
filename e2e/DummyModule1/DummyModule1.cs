using System.Threading;
using DummyModule.View;
using ModulesSample.Infrastructure;
using Prism.Avalonia.Infrastructure.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

namespace DummyModule
{
    public class DummyModule1 : IModule
    {
        private readonly IModuleTracker _moduleTracker;

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public DummyModule1(IModuleTracker moduleTracker, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _moduleTracker = moduleTracker;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _moduleTracker.RecordModuleConstructed(ModuleNames.ModuleDummy1);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _moduleTracker.RecordModuleLoaded(ModuleNames.ModuleDummy1);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Send Event messages for subscribers to react to
            Thread thread = new(new ThreadStart(DummyEventPublisher));
            thread.Start();

            _regionManager.RegisterViewWithRegion(RegionNames.Region1, typeof(DummyModuleView));
            _moduleTracker.RecordModuleInitialized(ModuleNames.ModuleDummy1);
        }

        /// <summary>Publish events to subscribers.</summary>
        private void DummyEventPublisher()
        {
            while (true)
            {
                Thread.Sleep(500);
                _eventAggregator.GetEvent<DummyEvent>().Publish();
            }
        }
    }
}
