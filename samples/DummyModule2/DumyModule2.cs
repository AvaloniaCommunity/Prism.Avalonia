using DummyModule2.View;
using ModulesSample.Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DummyModule2
{
    public class DummyModule2 : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        public DummyModule2(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            this.moduleTracker = moduleTracker;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.moduleTracker.RecordModuleInitialized(KnownModuleNames.ModuleDummy);
            regionManager.RegisterViewWithRegion("Region2", typeof(DummyModuleView2));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
