﻿using DummyModule2.View;
using ModulesSample.Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

namespace DummyModule2
{
    public class DummyModule2 : IModule
    {
        private readonly IModuleTracker _moduleTracker;
        private readonly IRegionManager _regionManager;

        public DummyModule2(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            _moduleTracker = moduleTracker;
            _regionManager = regionManager;

            _moduleTracker.RecordModuleConstructed(ModuleNames.ModuleDummy2);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _moduleTracker.RecordModuleInitialized(ModuleNames.ModuleDummy2);
            _regionManager.RegisterViewWithRegion(RegionNames.Region2, typeof(DummyModuleView2));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _moduleTracker.RecordModuleLoaded(ModuleNames.ModuleDummy2);
        }
    }
}
