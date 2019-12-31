using System;
using System.Windows;
using Autofac;
using Avalonia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.IocContainer.Avalonia.Tests.Support;

namespace Prism.Autofac.Avalonia.Tests
{
    [TestClass]
    public class AutofacBootstrapperNullContainerFixture : BootstrapperFixtureBase
    {
        [TestMethod]
        public void RunThrowsWhenNullContainerBuilderCreated()
        {
            var bootstrapper = new NullContainerBuilderBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "ContainerBuilder");
        }

        [TestMethod]
        public void RunThrowsWhenNullContainerCreated()
        {
            var bootstrapper = new NullContainerBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IContainer");
        }

        private class NullContainerBuilderBootstrapper : AutofacBootstrapper
        {
            protected override ContainerBuilder CreateContainerBuilder()
            {
                return null;
            }

            protected override IAvaloniaObject CreateShell()
            {
                throw new NotImplementedException();
            }

            protected override void InitializeShell()
            {
                throw new NotImplementedException();
            }
        }

        private class NullContainerBootstrapper : AutofacBootstrapper
        {
            protected override IContainer CreateContainer(ContainerBuilder builder)
            {
                return null;
            }

            protected override IAvaloniaObject CreateShell()
            {
                throw new NotImplementedException();
            }

            protected override void InitializeShell()
            {
                throw new NotImplementedException();
            }
        }
    }
}
