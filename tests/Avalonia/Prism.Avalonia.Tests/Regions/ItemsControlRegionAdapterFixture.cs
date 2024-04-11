﻿// TODO: 2022-07-12
// REF: https://github.com/AvaloniaUI/Avalonia/issues/7553
// Cannot perform the following. Check out, ContentControlRegionAdapterFixture.cs
// However, ItemsControl.Items is `IEnumerable` and doesn't play nicely.
//  `control.Items.Add(view);`
//  `control.Items[0]`
//  Needs Tested: `control.SetBinding(ItemsControl.ItemsSourceProperty, binding);`
/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation.Regions;
using Prism.Avalonia.Tests.Mocks;
using Avalonia.Controls;
using Avalonia.Data;
using Xunit;

namespace Prism.Avalonia.Tests.Regions
{
    public class ItemsControlRegionAdapterFixture
    {
        [StaFact]
        public void AdapterAssociatesItemsControlWithRegion()
        {
            var control = new ItemsControl();
            IRegionAdapter adapter = new TestableItemsControlRegionAdapter();

            IRegion region = adapter.Initialize(control, "Region1");
            Assert.NotNull(region);

            //// WPF: Assert.Same(control.ItemsSource, region.Views);
            Assert.Same(control.Items, region.Views);
        }

        [StaFact]
        public void AdapterAssignsARegionThatHasAllViewsActive()
        {
            ContainerLocator.SetContainerExtension(Mock.Of<IContainerExtension>());
            var control = new ItemsControl();
            IRegionAdapter adapter = new ItemsControlRegionAdapter(null);

            IRegion region = adapter.Initialize(control, "Region1");
            Assert.NotNull(region);
            Assert.IsType<AllActiveRegion>(region);
        }

        [StaFact]
        public void ShouldMoveAlreadyExistingContentInControlToRegion()
        {
            var control = new ItemsControl();
            var view = new object();
            control.Items.Add(view);
            IRegionAdapter adapter = new TestableItemsControlRegionAdapter();

            var region = (MockPresentationRegion)adapter.Initialize(control, "Region1");

            Assert.Single(region.MockViews);
            Assert.Same(view, region.MockViews.ElementAt(0));
            Assert.Same(view, control.Items[0]);
        }

        [StaFact]
        public void ControlWithExistingItemSourceThrows()
        {
            var control = new ItemsControl() { Items = new List<string>() };

            IRegionAdapter adapter = new TestableItemsControlRegionAdapter();

            try
            {
                var region = (MockPresentationRegion)adapter.Initialize(control, "Region1");
                //Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Contains("ItemsControl's ItemsSource property is not empty.", ex.Message);
            }
        }

        [StaFact]
        public void ControlWithExistingBindingOnItemsSourceWithNullValueThrows()
        {
            var control = new ItemsControl();
            Binding binding = new Binding("Enumerable");
            binding.Source = new SimpleModel() { Enumerable = null };
            // WPF: control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            // NEEDS TESTED - (From, Suess):
            control.SetValue(ItemsControl.ItemsProperty, binding);

            IRegionAdapter adapter = new TestableItemsControlRegionAdapter();

            try
            {
                var region = (MockPresentationRegion)adapter.Initialize(control, "Region1");
                //Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Contains("ItemsControl's ItemsSource property is not empty.", ex.Message);
            }
        }

        class SimpleModel
        {
            public IEnumerable Enumerable { get; set; }
        }

        private class TestableItemsControlRegionAdapter : ItemsControlRegionAdapter
        {
            public TestableItemsControlRegionAdapter() : base(null)
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
*/
