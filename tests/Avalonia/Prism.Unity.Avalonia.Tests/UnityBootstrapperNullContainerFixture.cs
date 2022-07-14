using System;
using Avalonia;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Prism.IocContainer.Avalonia.Tests.Support;

namespace Prism.Unity.Avalonia.Tests
{

    [TestClass]
    public class UnityBootstrapperNullContainerFixture : BootstrapperFixtureBase
    {
        [TestMethod]
        public void RunThrowsWhenNullContainerCreated()
        {
            var bootstrapper = new NullContainerBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IUnityContainer");
        }

        private class NullContainerBootstrapper : UnityBootstrapper
        {
            protected override IUnityContainer CreateContainer()
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