using System;
using Avalonia.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Avalonia.Tests.Mocks;
using Prism.Regions;
using Prism.Regions.Behaviors;

namespace Prism.Avalonia.Tests.Regions.Behaviors
{
    [TestClass]
    public class RegionActiveAwareBehaviorFixture
    {
        [TestMethod]
        public void SetsIsActivePropertyOnIActiveAwareObjects()
        {
            var region = new MockPresentationRegion();
            region.RegionManager = new MockRegionManager();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();
            var collection = region.MockActiveViews.Items;

            ActiveAwareControl activeAwareObject = new ActiveAwareControl();

            Assert.IsFalse(activeAwareObject.IsActive);
            collection.Add(activeAwareObject);

            Assert.IsTrue(activeAwareObject.IsActive);

            collection.Remove(activeAwareObject);
            Assert.IsFalse(activeAwareObject.IsActive);
        }

        [TestMethod]
        public void SetsIsActivePropertyOnIActiveAwareDataContexts()
        {
            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();
            var collection = region.MockActiveViews.Items;

            ActiveAwareControl activeAwareObject = new ActiveAwareControl();

            var ControlMock = new Mock<Control>();
            var Control = ControlMock.Object;
            Control.DataContext = activeAwareObject;

            Assert.IsFalse(activeAwareObject.IsActive);
            collection.Add(Control);

            Assert.IsTrue(activeAwareObject.IsActive);

            collection.Remove(Control);
            Assert.IsFalse(activeAwareObject.IsActive);
        }

        [TestMethod]
        public void SetsIsActivePropertyOnBothIActiveAwareViewAndDataContext()
        {
            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();
            var collection = region.MockActiveViews.Items;

            var activeAwareMock = new Mock<IActiveAware>();
            activeAwareMock.SetupProperty(o => o.IsActive);
            var activeAwareObject = activeAwareMock.Object;

            var ControlMock = new Mock<Control>();
            ControlMock.As<IActiveAware>().SetupProperty(o => o.IsActive);
            var Control = ControlMock.Object;
            Control.DataContext = activeAwareObject;

            Assert.IsFalse(((IActiveAware)Control).IsActive);
            Assert.IsFalse(activeAwareObject.IsActive);
            collection.Add(Control);

            Assert.IsTrue(((IActiveAware)Control).IsActive);
            Assert.IsTrue(activeAwareObject.IsActive);

            collection.Remove(Control);
            Assert.IsFalse(((IActiveAware)Control).IsActive);
            Assert.IsFalse(activeAwareObject.IsActive);
        }

        [TestMethod]
        public void DetachStopsListeningForChanges()
        {
            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            var collection = region.MockActiveViews.Items;
            behavior.Attach();
            behavior.Detach();
            ActiveAwareControl activeAwareObject = new ActiveAwareControl();

            collection.Add(activeAwareObject);

            Assert.IsFalse(activeAwareObject.IsActive);
        }

        [TestMethod]
        public void DoesNotThrowWhenAddingNonActiveAwareObjects()
        {
            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();
            var collection = region.MockActiveViews.Items;

            collection.Add(new object());
        }

        [TestMethod]
        public void DoesNotThrowWhenAddingNonActiveAwareDataContexts()
        {
            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();
            var collection = region.MockActiveViews.Items;

            var ControlMock = new Mock<Control>();
            var Control = ControlMock.Object;
            Control.DataContext = new object();


            collection.Add(Control);
        }

        [TestMethod]
        public void WhenParentViewGetsActivatedOrDeactivated_ThenChildViewIsNotUpdated()
        {
            var scopedRegionManager = new RegionManager();
            var scopedRegion = new Region { Name = "MyScopedRegion", RegionManager = scopedRegionManager };
            scopedRegionManager.Regions.Add(scopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = scopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new ActiveAwareControl();

            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, scopedRegionManager);
            region.Activate(view);

            scopedRegion.Add(childActiveAwareView);
            scopedRegion.Activate(childActiveAwareView);

            Assert.IsTrue(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsTrue(childActiveAwareView.IsActive);
        }

        [TestMethod]
        public void WhenParentViewGetsActivatedOrDeactivated_ThenSyncedChildViewIsUpdated()
        {
            var scopedRegionManager = new RegionManager();
            var scopedRegion = new Region { Name = "MyScopedRegion", RegionManager = scopedRegionManager };
            scopedRegionManager.Regions.Add(scopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = scopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new SyncedActiveAwareObject();

            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, scopedRegionManager);
            region.Activate(view);

            scopedRegion.Add(childActiveAwareView);
            scopedRegion.Activate(childActiveAwareView);

            Assert.IsTrue(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsFalse(childActiveAwareView.IsActive);
        }

        [TestMethod]
        public void WhenParentViewGetsActivatedOrDeactivated_ThenSyncedChildViewWithAttributeInVMIsUpdated()
        {
            var scopedRegionManager = new RegionManager();
            var scopedRegion = new Region { Name = "MyScopedRegion", RegionManager = scopedRegionManager };
            scopedRegionManager.Regions.Add(scopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = scopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new ActiveAwareControl();
            childActiveAwareView.DataContext = new SyncedActiveAwareObjectViewModel();

            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, scopedRegionManager);
            region.Activate(view);

            scopedRegion.Add(childActiveAwareView);
            scopedRegion.Activate(childActiveAwareView);

            Assert.IsTrue(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsFalse(childActiveAwareView.IsActive);
        }

        [TestMethod]
        public void WhenParentViewGetsActivatedOrDeactivated_ThenSyncedChildViewModelThatIsNotAControlIsNotUpdated()
        {
            var scopedRegionManager = new RegionManager();
            var scopedRegion = new Region { Name = "MyScopedRegion", RegionManager = scopedRegionManager };
            scopedRegionManager.Regions.Add(scopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = scopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new ActiveAwareObject();            

            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, scopedRegionManager);
            region.Activate(view);

            scopedRegion.Add(childActiveAwareView);
            scopedRegion.Activate(childActiveAwareView);

            Assert.IsTrue(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsTrue(childActiveAwareView.IsActive);
        }

        [TestMethod]
        public void WhenParentViewGetsActivatedOrDeactivated_ThenSyncedChildViewNotInActiveViewsIsNotUpdated()
        {
            var scopedRegionManager = new RegionManager();
            var scopedRegion = new Region { Name="MyScopedRegion", RegionManager = scopedRegionManager };
            scopedRegionManager.Regions.Add(scopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = scopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new SyncedActiveAwareObject();

            var region = new MockPresentationRegion();
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, scopedRegionManager);
            region.Activate(view);

            scopedRegion.Add(childActiveAwareView);
            scopedRegion.Deactivate(childActiveAwareView);

            Assert.IsFalse(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsFalse(childActiveAwareView.IsActive);

            region.Activate(view);

            Assert.IsFalse(childActiveAwareView.IsActive);
        }

        [TestMethod]
        public void WhenParentViewWithoutScopedRegionGetsActivatedOrDeactivated_ThenSyncedChildViewIsNotUpdated()
        {
            var commonRegionManager = new RegionManager();
            var nonScopedRegion = new Region { Name="MyRegion", RegionManager = commonRegionManager };
            commonRegionManager.Regions.Add(nonScopedRegion);
            var behaviorForScopedRegion = new RegionActiveAwareBehavior { Region = nonScopedRegion };
            behaviorForScopedRegion.Attach();
            var childActiveAwareView = new SyncedActiveAwareObject();

            var region = new MockPresentationRegion { RegionManager = commonRegionManager };
            var behavior = new RegionActiveAwareBehavior { Region = region };
            behavior.Attach();

            var view = new MockControl();
            region.Add(view);
            RegionManager.SetRegionManager(view, commonRegionManager);
            region.Activate(view);

            nonScopedRegion.Add(childActiveAwareView);
            nonScopedRegion.Activate(childActiveAwareView);

            Assert.IsTrue(childActiveAwareView.IsActive);

            region.Deactivate(view);

            Assert.IsTrue(childActiveAwareView.IsActive);
        }

        class ActiveAwareObject : IActiveAware
        {
            public bool IsActive { get; set; }
            public event EventHandler IsActiveChanged;
        }

        class ActiveAwareControl : Control, IActiveAware
        {
            public bool IsActive { get; set; }
            public event EventHandler IsActiveChanged;
        }

        [SyncActiveState]
        class SyncedActiveAwareObject : IActiveAware
        {
            public bool IsActive { get; set; }
            public event EventHandler IsActiveChanged;
        }

        [SyncActiveState]
        class SyncedActiveAwareObjectViewModel : IActiveAware
        {
            public bool IsActive { get; set; }
            public event EventHandler IsActiveChanged;
        }
        
    }
}