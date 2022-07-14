using Avalonia;
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Prism.Logging;
using Prism.Regions;

namespace Prism.Unity.Avalonia.Tests
{
    [TestClass]
    public class UnityBootstrapperNullModuleManagerFixture
    {
        [TestMethod]
        public void RunShouldNotCallInitializeModulesWhenModuleManagerNotFound()
        {
            var bootstrapper = new NullModuleManagerBootstrapper();

            bootstrapper.Run();

            Assert.IsFalse(bootstrapper.InitializeModulesCalled);
        }

        private class NullModuleManagerBootstrapper : UnityBootstrapper
        {
            public bool InitializeModulesCalled;

            protected override void ConfigureContainer()
            {
                //base.RegisterDefaultTypesIfMissing();

                Container.RegisterInstance<ILoggerFacade>(Logger);

                this.Container.RegisterInstance(this.ModuleCatalog);
                RegisterTypeIfMissing(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), true);
            }

            protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
            {
                return null;
            }

            protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
            {
                return null;
            }

            protected override IAvaloniaObject CreateShell()
            {
                return null;
            }

            protected override void InitializeModules()
            {
                this.InitializeModulesCalled = true;
            }
        }
    }
}
