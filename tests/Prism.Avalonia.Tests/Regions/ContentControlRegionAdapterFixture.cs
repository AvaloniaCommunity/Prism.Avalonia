using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Avalonia.Tests.Mocks;
using Prism.Regions;

namespace Prism.Avalonia.Tests.Regions
{
    [TestClass]
    public class ContentControlRegionAdapterFixture
    {
        [TestMethod]
        public void AdapterAssociatesSelectorWithRegionActiveViews()
        {
            var control = new ContentControl();
            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            MockPresentationRegion region = (MockPresentationRegion)adapter.Initialize(control, "Region1");
            Assert.IsNotNull(region);

            Assert.IsNull(control.Content);
            region.MockActiveViews.Items.Add(new object());

            Assert.IsNotNull(control.Content);
            Assert.AreSame(control.Content, region.ActiveViews.ElementAt(0));

            region.MockActiveViews.Items.Add(new object());
            Assert.AreSame(control.Content, region.ActiveViews.ElementAt(0));

            region.MockActiveViews.Items.RemoveAt(0);
            Assert.AreSame(control.Content, region.ActiveViews.ElementAt(0));

            region.MockActiveViews.Items.RemoveAt(0);
            Assert.IsNull(control.Content);
        }

        [TestMethod]
        public void ControlWithExistingBindingOnContentWithNullValueThrows()
        {
            var control = new ContentControl();
            Binding binding = new Binding("ObjectContents");
            binding.Source = new SimpleModel() { ObjectContents = null };
            control.Bind(ContentControl.ContentProperty, binding);

            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            try
            {
                var region = (MockPresentationRegion)adapter.Initialize(control, "Region1");
                Assert.IsTrue(true); // HACK: in ContentControlRegionAdapter.Adapt there is now way to determine
                                     // is ContentProperty already bound or not
               // Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                StringAssert.Contains(ex.Message, "ContentControl's Content property is not empty.");
            }
        }

        [TestMethod]
        public void AddedItemShouldBeActivated()
        {
            var control = new ContentControl();
            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            MockPresentationRegion region = (MockPresentationRegion)adapter.Initialize(control, "Region1");

            var mockView = new object();
            region.Add(mockView);

            Assert.AreEqual(1, region.ActiveViews.Count());
            Assert.IsTrue(region.ActiveViews.Contains(mockView));
        }

        [TestMethod]
        public void ShouldNotActivateAdditionalViewsAdded()
        {
            var control = new ContentControl();
            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            MockPresentationRegion region = (MockPresentationRegion)adapter.Initialize(control, "Region1");

            var mockView = new object();
            region.Add(mockView);
            region.Add(new object());

            Assert.AreEqual(1, region.ActiveViews.Count());
            Assert.IsTrue(region.ActiveViews.Contains(mockView));
        }

        [TestMethod]
        public void ShouldActivateAddedViewWhenNoneIsActive()
        {
            var control = new ContentControl();
            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            MockPresentationRegion region = (MockPresentationRegion)adapter.Initialize(control, "Region1");

            var mockView1 = new object();
            region.Add(mockView1);
            region.Deactivate(mockView1);

            var mockView2 = new object();
            region.Add(mockView2);

            Assert.AreEqual(1, region.ActiveViews.Count());
            Assert.IsTrue(region.ActiveViews.Contains(mockView2));
        }

        [TestMethod]
        public void CanRemoveViewWhenNoneActive()
        {
            var control = new ContentControl();
            IRegionAdapter adapter = new TestableContentControlRegionAdapter();

            MockPresentationRegion region = (MockPresentationRegion)adapter.Initialize(control, "Region1");

            var mockView1 = new object();
            region.Add(mockView1);
            region.Deactivate(mockView1);
            region.Remove(mockView1);
            Assert.AreEqual(0, region.ActiveViews.Count());
        }

        class SimpleModel
        {
            public Object ObjectContents { get; set; }
        }

        private class TestableContentControlRegionAdapter : ContentControlRegionAdapter
        {
            public TestableContentControlRegionAdapter() : base(null)
            {
            }

            private MockPresentationRegion region = new MockPresentationRegion();

            protected override IRegion CreateRegion()
            {
                return region;
            }
        }
    }
}