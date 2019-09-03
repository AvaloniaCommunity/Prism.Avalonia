

using System;
using System.Linq;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Mvvm;
using Prism.Avalonia.Tests.Mocks.Views;
using Prism.Avalonia.Tests.Mocks.ViewModels;
using Prism.Ioc;

namespace Prism.Avalonia.Tests
{
    [TestClass]
    public class BootstrapperFixture
    {
        [TestMethod]
        public void LoggerDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseLogger);
        }

        [TestMethod]
        public void ModuleCatalogDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseModuleCatalog);
        }

        [TestMethod]
        public void ShellDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseShell);
        }

        [TestMethod]
        public void CreateLoggerInitializesLogger()
        {
            var bootstrapper = new DefaultBootstrapper();
            bootstrapper.CallCreateLogger();

            Assert.IsNotNull(bootstrapper.BaseLogger);

            Assert.IsInstanceOfType(bootstrapper.BaseLogger, typeof(TextLogger));
        }

        [TestMethod]
        public void ConfigureViewModelLocatorShouldUserServiceLocatorAsResolver()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorForViewModelLocator();

            bootstrapper.CallConfigureViewModelLocator();

            var view = new MockView();

            ViewModelLocationProvider.AutoWireViewModelChanged(view, (v, vm) =>
                {
                    Assert.IsNotNull(v);
                    Assert.IsNotNull(vm);
                    Assert.IsInstanceOfType(vm, typeof(MockViewModel));
                });
        }

        private static void CreateAndConfigureServiceLocatorForViewModelLocator()
        {
            var serviceLocator = new Prism.Avalonia.Tests.ServiceLocatorExtensionsFixture.MockServiceLocator(() => new MockViewModel());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        [TestMethod]
        public void CreateModuleCatalogShouldInitializeModuleCatalog()
        {
            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.CallCreateModuleCatalog();

            Assert.IsNotNull(bootstrapper.BaseModuleCatalog);
        }

        [TestMethod]
        public void RegisterFrameworkExceptionTypesShouldRegisterActivationException()
        {
            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.CallRegisterFrameworkExceptionTypes();

            Assert.IsTrue(ExceptionExtensions.IsFrameworkExceptionRegistered(
                typeof(ActivationException)));
        }

        [TestMethod]
        public void ConfigureRegionAdapterMappingsShouldRegisterItemsControlMapping()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithRegionAdapters();

            var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

            Assert.IsNotNull(regionAdapterMappings);
            Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(ItemsControl)));
        }

        [TestMethod]
        public void ConfigureRegionAdapterMappingsShouldRegisterContentControlMapping()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithRegionAdapters();

            var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

            Assert.IsNotNull(regionAdapterMappings);
            Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(ContentControl)));
        }

        [TestMethod]
        public void ConfigureRegionAdapterMappingsShouldRegisterSelectorMapping()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithRegionAdapters();

            var regionAdapterMappings = bootstrapper.CallConfigureRegionAdapterMappings();

            Assert.IsNotNull(regionAdapterMappings);
            //Assert.IsNotNull(regionAdapterMappings.GetMapping(typeof(Selector)));
        }

        private static void CreateAndConfigureServiceLocatorWithRegionAdapters()
        {
            Mock<ServiceLocatorImplBase> serviceLocator = new Mock<ServiceLocatorImplBase>();
            var regionBehaviorFactory = new RegionBehaviorFactory(serviceLocator.Object);
            serviceLocator.Setup(sl => sl.GetInstance<RegionAdapterMappings>()).Returns(new RegionAdapterMappings());
            //serviceLocator.Setup(sl => sl.GetInstance<SelectorRegionAdapter>()).Returns(new SelectorRegionAdapter(regionBehaviorFactory));
            serviceLocator.Setup(sl => sl.GetInstance<ItemsControlRegionAdapter>()).Returns(new ItemsControlRegionAdapter(regionBehaviorFactory));
            serviceLocator.Setup(sl => sl.GetInstance<ContentControlRegionAdapter>()).Returns(new ContentControlRegionAdapter(regionBehaviorFactory));

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddSevenDefaultBehaviors()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.AreEqual(7, bootstrapper.DefaultRegionBehaviorTypes.Count());
        }

        private static void CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors()
        {
            Mock<ServiceLocatorImplBase> serviceLocator = new Mock<ServiceLocatorImplBase>();
            var regionBehaviorFactory = new RegionBehaviorFactory(serviceLocator.Object);
            serviceLocator.Setup(sl => sl.GetInstance<IRegionBehaviorFactory>()).Returns(new RegionBehaviorFactory(serviceLocator.Object));

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddAutoPopulateRegionBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(AutoPopulateRegionBehavior.BehaviorKey));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldBindRegionContextToDependencyObjectBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            //Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(BindRegionContextToDependencyObjectBehavior.BehaviorKey));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddRegionActiveAwareBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionActiveAwareBehavior.BehaviorKey));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddSyncRegionContextWithHostBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(SyncRegionContextWithHostBehavior.BehaviorKey));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddRegionManagerRegistrationBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionManagerRegistrationBehavior.BehaviorKey));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddRegionLifetimeBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(RegionMemberLifetimeBehavior.BehaviorKey));
        }

        [TestMethod]
        public void OnInitializedShouldRunLast()
        {
            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.Run();

            Assert.IsTrue(bootstrapper.ExtraInitialization);
        }
    }

    internal class DefaultBootstrapper : Bootstrapper
    {
        public IRegionBehaviorFactory DefaultRegionBehaviorTypes;
        public bool ExtraInitialization;

        public ILoggerFacade BaseLogger
        {
            get
            {
                return base.Logger;
            }
        }

        public IModuleCatalog BaseModuleCatalog
        {
            get { return base.ModuleCatalog; }
            set { base.ModuleCatalog = value; }
        }

        public IAvaloniaObject BaseShell
        {
            get { return base.Shell; }
            set { base.Shell = value; }
        }

        public void CallCreateLogger()
        {
            this.Logger = base.CreateLogger();
        }

        public void CallCreateModuleCatalog()
        {
            this.ModuleCatalog = base.CreateModuleCatalog();
        }

        public RegionAdapterMappings CallConfigureRegionAdapterMappings()
        {
            return base.ConfigureRegionAdapterMappings();
        }

        public void CallConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            Assert.IsFalse(this.ExtraInitialization);
        }

        protected override void OnInitialized()
        {
            this.ExtraInitialization = true;
        }

        protected override IAvaloniaObject CreateShell()
        {
            throw new NotImplementedException();
        }

        protected override void InitializeShell()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureServiceLocator()
        {
            throw new NotImplementedException();
        }

        public void CallRegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();
        }

        public IRegionBehaviorFactory CallConfigureDefaultRegionBehaviors()
        {
            this.DefaultRegionBehaviorTypes = base.ConfigureDefaultRegionBehaviors();
            return this.DefaultRegionBehaviorTypes;
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return null;
        }
    }

}
