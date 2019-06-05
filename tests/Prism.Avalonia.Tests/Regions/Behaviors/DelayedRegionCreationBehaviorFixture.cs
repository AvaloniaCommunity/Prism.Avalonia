using System;
using System.Linq;
using Avalonia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Avalonia.Tests.Mocks;
using Prism.Regions;
using Prism.Regions.Behaviors;

namespace Prism.Avalonia.Tests.Regions.Behaviors
{
    [TestClass]
    public class DelayedRegionCreationBehaviorFixture
    {
        private DelayedRegionCreationBehavior GetBehavior(StyledProperty control, MockRegionManagerAccessor accessor, MockRegionAdapter adapter)
        {
            var mappings = new RegionAdapterMappings();
            mappings.RegisterMapping(control.GetType(), adapter);
            var behavior = new DelayedRegionCreationBehavior(mappings);
            behavior.RegionManagerAccessor = accessor;
            behavior.TargetElement = control;
            return behavior;
        }


        private DelayedRegionCreationBehavior GetBehavior(StyledProperty control, MockRegionManagerAccessor accessor)
        {
            return GetBehavior(control, accessor, new MockRegionAdapter());
        }

        [TestMethod]
        public void RegionWillNotGetCreatedTwiceWhenThereAreMoreRegions()
        {
            var control1 = new MockControl();
            var control2 = new MockControl();

            var accessor = new MockRegionManagerAccessor
                               {
                                   GetRegionName = d => d == control1 ? "Region1" : "Region2"
                               };

            var adapter = new MockRegionAdapter();
            adapter.Accessor = accessor;

            var behavior1 = this.GetBehavior(control1, accessor, adapter);
            var behavior2 = this.GetBehavior(control2, accessor, adapter);

            behavior1.Attach();
            behavior2.Attach();

            accessor.UpdateRegions();

            Assert.IsTrue(adapter.CreatedRegions.Contains("Region1"));
            Assert.IsTrue(adapter.CreatedRegions.Contains("Region2"));
            Assert.AreEqual(1, adapter.CreatedRegions.Count((name) => name == "Region2"));

        }


        [TestMethod]
        public void RegionGetsCreatedWhenAccessingRegions()
        {
            var control = new MockControl();

            var accessor = new MockRegionManagerAccessor
                               {
                                   GetRegionName = d => "myRegionName"
                               };

            var behavior = this.GetBehavior(control, accessor);
            behavior.Attach();

            accessor.UpdateRegions();

            Assert.IsNotNull(RegionManager.GetObservableRegion(control).Value);
            Assert.IsInstanceOfType(RegionManager.GetObservableRegion(control).Value, typeof(IRegion));
        }

        [TestMethod]
        public void RegionDoesNotGetCreatedTwiceWhenUpdatingRegions()
        {
            var control = new MockControl();

            var accessor = new MockRegionManagerAccessor
            {
                GetRegionName = d => "myRegionName"
            };

            var behavior = this.GetBehavior(control, accessor);
            behavior.Attach();
            accessor.UpdateRegions();
            IRegion region = RegionManager.GetObservableRegion(control).Value;

            accessor.UpdateRegions();

            Assert.AreSame(region, RegionManager.GetObservableRegion(control).Value);
        }

        [TestMethod]
        public void BehaviorShouldUnhookEventWhenDetaching()
        {
            var control = new MockControl();

            var accessor = new MockRegionManagerAccessor
                               {
                                   GetRegionName = d => "myRegionName",
                               };
            var behavior = this.GetBehavior(control, accessor);
            behavior.Attach();

            int startingCount = accessor.GetSubscribersCount();

            behavior.Detach();

            Assert.AreEqual<int>(startingCount - 1, accessor.GetSubscribersCount());
        }
    }
}