using System;
using Prism.Container.Wpf.Mocks;
using Prism.IocContainer.Wpf.Tests.Support;
using Xunit;

namespace Prism.DryIoc.Avalonia.Tests.Fixtures.Bootstrapper
{
    public class BootstrapperNullModuleCatalogFixture : BootstrapperFixtureBase
    {
        [Fact]
        public void NullModuleCatalogThrowsOnDefaultModuleInitialization()
        {
            var bootstrapper = new NullModuleCatalogBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IModuleCatalog");
        }
    }
}
