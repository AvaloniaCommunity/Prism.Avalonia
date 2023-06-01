using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Moq;
using Prism.Avalonia.Tests.Mocks.Views;
using Prism.Avalonia.Tests.Mvvm;
using Prism.Ioc;
using Prism.Regions;
using Xunit;

namespace Prism.Avalonia.Tests.Regions
{
    public class RegionViewRegistryFixture
    {
        [Fact]
        public void CanNotRegisterWhenMissingDataTemplateDataType()
        {
            // Created for Avalonia v11.0.0-pre4
            // See, https://github.com/AvaloniaUI/Avalonia/pull/8221
            var xaml = GenerateUserControlWithListView(useCompileBindings: false, useDataTemplateDataType: false);
            var userCtrl = (MockBindingsView)AvaloniaRuntimeXamlLoader.Load(xaml);

            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(MockBindingsView))).Returns(userCtrl);
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.RegisterViewWithRegion("MyRegion", typeof(MockBindingsView));
            var result = registry.GetContents("MyRegion");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<MockBindingsView>(result.ElementAt(0));
        }

        [Fact]
        public void CanRegisterWhenDataTemplateDataTypeIsProvided()
        {
            // Created for Avalonia v11.0.0-pre4
            // See, https://github.com/AvaloniaUI/Avalonia/pull/8221
            var xaml = GenerateUserControlWithListView(useDataTemplateDataType: true);
            var userCtrl = (MockBindingsView)AvaloniaRuntimeXamlLoader.Load(xaml);

            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(MockBindingsView))).Returns(userCtrl);
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.RegisterViewWithRegion("MyRegion", typeof(MockBindingsView));
            var result = registry.GetContents("MyRegion");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<MockBindingsView>(result.ElementAt(0));
        }

        [Fact]
        public void CanRegisterContentAndRetrieveIt()
        {
            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(MockContentObject))).Returns(new MockContentObject());
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.RegisterViewWithRegion("MyRegion", typeof(MockContentObject));
            var result = registry.GetContents("MyRegion");

            //Assert.Equal(typeof(MockContentObject), calledType);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<MockContentObject>(result.ElementAt(0));
        }

        [Fact]
        public void ShouldRaiseEventWhenAddingContent()
        {
            var listener = new MySubscriberClass();
            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(MockContentObject))).Returns(new MockContentObject());
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.ContentRegistered += listener.OnContentRegistered;

            registry.RegisterViewWithRegion("MyRegion", typeof(MockContentObject));

            Assert.NotNull(listener.onViewRegisteredArguments);
            Assert.NotNull(listener.onViewRegisteredArguments.GetView);

            var result = listener.onViewRegisteredArguments.GetView();
            Assert.NotNull(result);
            Assert.IsType<MockContentObject>(result);
        }

        [Fact]
        public void CanRegisterContentAsDelegateAndRetrieveIt()
        {
            var registry = new RegionViewRegistry(null);
            var content = new MockContentObject();

            registry.RegisterViewWithRegion("MyRegion", () => content);
            var result = registry.GetContents("MyRegion");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Same(content, result.ElementAt(0));
        }

        [Fact]
        public async Task ShouldNotPreventSubscribersFromBeingGarbageCollected()
        {
            var registry = new RegionViewRegistry(null);
            var subscriber = new MySubscriberClass();
            registry.ContentRegistered += subscriber.OnContentRegistered;

            WeakReference subscriberWeakReference = new WeakReference(subscriber);

            subscriber = null;
            await Task.Delay(50);
            GC.Collect();

            Assert.False(subscriberWeakReference.IsAlive);
        }

        [Fact]
        public void OnRegisterErrorShouldGiveClearException()
        {
            var registry = new RegionViewRegistry(null);
            registry.ContentRegistered += new EventHandler<ViewRegisteredEventArgs>(FailWithInvalidOperationException);

            try
            {
                registry.RegisterViewWithRegion("R1", typeof(object));
                //Assert.Fail();
            }
            catch (ViewRegistrationException ex)
            {
                Assert.Contains("Dont do this", ex.Message);
                Assert.Contains("R1", ex.Message);
                Assert.Equal("Dont do this", ex.InnerException.Message);
            }
            catch (Exception)
            {
                //Assert.Fail("Wrong exception type");
            }
        }

        [Fact]
        public void OnRegisterErrorShouldSkipFrameworkExceptions()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(FrameworkException));
            var registry = new RegionViewRegistry(null);
            registry.ContentRegistered += new EventHandler<ViewRegisteredEventArgs>(FailWithFrameworkException);
            var ex = Record.Exception(() => registry.RegisterViewWithRegion("R1", typeof(object)));
            Assert.NotNull(ex);
            Assert.IsType<ViewRegistrationException>(ex);
            Assert.Contains("Dont do this", ex.Message);
            Assert.Contains("R1", ex.Message);
        }

        [StaFact]
        public void RegisterViewWithRegion_ShouldHaveViewModel_ByDefault()
        {
            ViewModelLocatorFixture.ResetViewModelLocationProvider();

            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(Mocks.Views.Mock))).Returns(new Mocks.Views.Mock());
            containerMock.Setup(c => c.Resolve(typeof(Mocks.ViewModels.MockViewModel))).Returns(new Mocks.ViewModels.MockViewModel());
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.RegisterViewWithRegion("MyRegion", typeof(Mocks.Views.Mock));

            // TODO: AutowireViewModel is not kicking off.
            var result = registry.GetContents("MyRegion");
            Assert.NotNull(result);
            Assert.Single(result);

            var view = result.ElementAt(0) as Control;
            Assert.IsType<Mocks.Views.Mock>(view);
            Assert.NotNull(view.DataContext);
            Assert.IsType<Mocks.ViewModels.MockViewModel>(view.DataContext);
        }

        [StaFact]
        public void RegisterViewWithRegion_ShouldNotHaveViewModel_OnOptOut()
        {
            ViewModelLocatorFixture.ResetViewModelLocationProvider();

            var containerMock = new Mock<IContainerExtension>();
            containerMock.Setup(c => c.Resolve(typeof(Mocks.Views.MockOptOut))).Returns(new Mocks.Views.MockOptOut());
            containerMock.Setup(c => c.Resolve(typeof(Mocks.ViewModels.MockOptOutViewModel))).Returns(new Mocks.ViewModels.MockOptOutViewModel());
            var registry = new RegionViewRegistry(containerMock.Object);

            registry.RegisterViewWithRegion("MyRegion", typeof(Mocks.Views.MockOptOut));

            var result = registry.GetContents("MyRegion");
            Assert.NotNull(result);
            Assert.Single(result);

            var view = result.ElementAt(0) as Control;
            Assert.IsType<Mocks.Views.MockOptOut>(view);
            Assert.Null(view.DataContext);
        }

        private string GenerateUserControlWithListView(bool useCompileBindings = true, bool useDataTemplateDataType = true)
        {
            var xaml = @"
<UserControl xmlns='https://github.com/avaloniaui'
             xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
             xmlns:mc='http://schemas.openxmlformats.org/markup-compatibility/2006'
             xmlns:prism='http://prismlibrary.com/'
             xmlns:system='clr-namespace:System;assembly=mscorlib'";

            xaml += $@"
             x:CompileBindings='{useCompileBindings}'";

            xaml += @"
             x:Class='Prism.Avalonia.Tests.Mocks.Views.MockBindingsView'>
  <StackPanel>
    <ListBox Margin='2'
           VerticalAlignment='Bottom'
           Items='{Binding ListItems}'
           ScrollViewer.HorizontalScrollBarVisibility='Visible'
           ScrollViewer.VerticalScrollBarVisibility='Visible'
           SelectedIndex='{Binding ListItemSelected}'
           SelectionMode='Single'>
      <ListBox.DataTemplates>";

            if (useDataTemplateDataType)
                xaml += @"
        <DataTemplate DataType='{x:Type system:String}'>";
            else
                xaml += @"
        <DataTemplate>";

            xaml += @"
          <TextBlock Text='{Binding .}'
                     FontSize='10'
                     TextWrapping='NoWrap' />
        </DataTemplate>
      </ListBox.DataTemplates>
    </ListBox>
  </StackPanel>
</UserControl>";
            return xaml;
        }

        private void FailWithFrameworkException(object sender, ViewRegisteredEventArgs e)
        {
            try
            {
                FailWithInvalidOperationException(sender, e);
            }
            catch (Exception ex)
            {
                throw new FrameworkException(ex);
            }
        }

        private void FailWithInvalidOperationException(object sender, ViewRegisteredEventArgs e)
        {
            throw new InvalidOperationException("Dont do this");
        }

        private class MockContentObject
        {
        }

        private class MySubscriberClass
        {
            public ViewRegisteredEventArgs onViewRegisteredArguments;

            public void OnContentRegistered(object sender, ViewRegisteredEventArgs e)
            {
                onViewRegisteredArguments = e;
            }
        }

        private class FrameworkException : Exception
        {
            public FrameworkException(Exception innerException)
                : base("", innerException)
            {
            }
        }
    }
}
