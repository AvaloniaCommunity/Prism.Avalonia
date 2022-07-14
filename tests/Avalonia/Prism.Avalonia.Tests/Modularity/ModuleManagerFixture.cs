using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Prism.Avalonia.Tests.Mocks;
using Prism.Ioc;
using Prism.Modularity;
using Xunit;

namespace Prism.Avalonia.Tests.Modularity
{
    public class ModuleManagerFixture
    {
        [Fact]
        public void NullLoaderThrows()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                new ModuleManager(null, new MockModuleCatalog());
            });
        }

        [Fact]
        public void NullCatalogThrows()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                new ModuleManager(new MockModuleInitializer(), null);
            });
        }

        [Fact]
        public void ShouldInvokeRetrieverForModules()
        {
            var loader = new MockModuleInitializer();
            var moduleInfo = CreateModuleInfo("needsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new MockModuleCatalog { Modules = { moduleInfo } };
            ModuleManager manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };

            manager.Run();

            Assert.Contains(moduleInfo, moduleTypeLoader.LoadedModules);
        }

        [Fact]
        public void ShouldInitializeModulesOnRetrievalCompleted()
        {
            var loader = new MockModuleInitializer();
            var backgroungModuleInfo = CreateModuleInfo("NeedsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new MockModuleCatalog { Modules = { backgroungModuleInfo } };
            ModuleManager manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };
            Assert.False(loader.InitializeCalled);

            manager.Run();

            Assert.True(loader.InitializeCalled);
            Assert.Single(loader.InitializedModules);
            Assert.Equal(backgroungModuleInfo, loader.InitializedModules[0]);
        }

        [Fact]
        public void ShouldInitializeModuleOnDemand()
        {
            var loader = new MockModuleInitializer();
            var onDemandModule = CreateModuleInfo("NeedsRetrieval", InitializationMode.OnDemand);
            var catalog = new MockModuleCatalog { Modules = { onDemandModule } };
            ModuleManager manager = new ModuleManager(loader, catalog);
            var moduleRetriever = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleRetriever };
            manager.Run();

            Assert.False(loader.InitializeCalled);
            Assert.Empty(moduleRetriever.LoadedModules);

            manager.LoadModule("NeedsRetrieval");

            Assert.Single(moduleRetriever.LoadedModules);
            Assert.True(loader.InitializeCalled);
            Assert.Single(loader.InitializedModules);
            Assert.Equal(onDemandModule, loader.InitializedModules[0]);
        }

        [Fact]
        public void InvalidOnDemandModuleNameThrows()
        {
            var ex = Assert.Throws<ModuleNotFoundException>(() =>
            {
                var loader = new MockModuleInitializer();

                var catalog = new MockModuleCatalog { Modules = new List<IModuleInfo> { CreateModuleInfo("Missing", InitializationMode.OnDemand) } };

                ModuleManager manager = new ModuleManager(loader, catalog);
                var moduleTypeLoader = new MockModuleTypeLoader();

                manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };
                manager.Run();

                manager.LoadModule("NonExistent");
            });
        }

        [Fact]
        public void EmptyOnDemandModuleReturnedThrows()
        {
            var ex = Assert.Throws<ModuleNotFoundException>(() =>
            {
                var loader = new MockModuleInitializer();

                var catalog = new MockModuleCatalog { CompleteListWithDependencies = modules => new List<ModuleInfo>() };
                ModuleManager manager = new ModuleManager(loader, catalog);
                var moduleRetriever = new MockModuleTypeLoader();
                manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleRetriever };
                manager.Run();

                manager.LoadModule("NullModule");
            });
        }

        [Fact]
        public void ShouldNotLoadTypeIfModuleInitialized()
        {
            var loader = new MockModuleInitializer();
            var alreadyPresentModule = CreateModuleInfo(typeof(MockModule), InitializationMode.WhenAvailable);
            alreadyPresentModule.State = ModuleState.ReadyForInitialization;
            var catalog = new MockModuleCatalog { Modules = { alreadyPresentModule } };
            var manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };

            manager.Run();

            Assert.DoesNotContain(alreadyPresentModule, moduleTypeLoader.LoadedModules);
            Assert.True(loader.InitializeCalled);
            Assert.Single(loader.InitializedModules);
            Assert.Equal(alreadyPresentModule, loader.InitializedModules[0]);
        }

        [Fact]
        public void ShouldNotLoadSameModuleTwice()
        {
            var loader = new MockModuleInitializer();
            var onDemandModule = CreateModuleInfo(typeof(MockModule), InitializationMode.OnDemand);
            var catalog = new MockModuleCatalog { Modules = { onDemandModule } };
            var manager = new ModuleManager(loader, catalog);
            manager.Run();
            manager.LoadModule("MockModule");
            loader.InitializeCalled = false;
            manager.LoadModule("MockModule");

            Assert.False(loader.InitializeCalled);
        }

        [Fact]
        public void ShouldNotLoadModuleThatNeedsRetrievalTwice()
        {
            var loader = new MockModuleInitializer();
            var onDemandModule = CreateModuleInfo("ModuleThatNeedsRetrieval", InitializationMode.OnDemand);
            var catalog = new MockModuleCatalog { Modules = { onDemandModule } };
            var manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };
            manager.Run();
            manager.LoadModule("ModuleThatNeedsRetrieval");
            moduleTypeLoader.RaiseLoadModuleCompleted(new LoadModuleCompletedEventArgs(onDemandModule, null));
            loader.InitializeCalled = false;

            manager.LoadModule("ModuleThatNeedsRetrieval");

            Assert.False(loader.InitializeCalled);
        }

        [Fact]
        public void ShouldCallValidateCatalogBeforeGettingGroupsFromCatalog()
        {
            var loader = new MockModuleInitializer();
            var catalog = new MockModuleCatalog();
            var manager = new ModuleManager(loader, catalog);
            bool validateCatalogCalled = false;
            bool getModulesCalledBeforeValidate = false;

            catalog.ValidateCatalog = () => validateCatalogCalled = true;
            catalog.CompleteListWithDependencies = f =>
            {
                if (!validateCatalogCalled)
                {
                    getModulesCalledBeforeValidate = true;
                }

                return null;
            };
            manager.Run();

            Assert.True(validateCatalogCalled);
            Assert.False(getModulesCalledBeforeValidate);
        }

        [Fact]
        public void ShouldNotInitializeIfDependenciesAreNotMet()
        {
            var loader = new MockModuleInitializer();
            var requiredModule = CreateModuleInfo("ModuleThatNeedsRetrieval1", InitializationMode.WhenAvailable);
            requiredModule.ModuleName = "RequiredModule";
            var dependantModuleInfo = CreateModuleInfo("ModuleThatNeedsRetrieval2", InitializationMode.WhenAvailable, "RequiredModule");

            var catalog = new MockModuleCatalog { Modules = { requiredModule, dependantModuleInfo } };
            catalog.GetDependentModules = m => new[] { requiredModule };

            ModuleManager manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };

            manager.Run();

            moduleTypeLoader.RaiseLoadModuleCompleted(new LoadModuleCompletedEventArgs(dependantModuleInfo, null));

            Assert.False(loader.InitializeCalled);
            Assert.Empty(loader.InitializedModules);
        }

        [Fact]
        public void ShouldInitializeIfDependenciesAreMet()
        {
            var initializer = new MockModuleInitializer();
            var requiredModule = CreateModuleInfo("ModuleThatNeedsRetrieval1", InitializationMode.WhenAvailable);
            requiredModule.ModuleName = "RequiredModule";
            var dependantModuleInfo = CreateModuleInfo("ModuleThatNeedsRetrieval2", InitializationMode.WhenAvailable, "RequiredModule");

            var catalog = new MockModuleCatalog { Modules = { requiredModule, dependantModuleInfo } };
            catalog.GetDependentModules = delegate (IModuleInfo module)
            {
                if (module == dependantModuleInfo)
                    return new[] { requiredModule };
                else
                    return null;
            };

            ModuleManager manager = new ModuleManager(initializer, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };

            manager.Run();

            Assert.True(initializer.InitializeCalled);
            Assert.Equal(2, initializer.InitializedModules.Count);
        }

        [Fact]
        public void ShouldThrowOnRetrieverErrorAndWrapException()
        {
            var loader = new MockModuleInitializer();
            var moduleInfo = CreateModuleInfo("NeedsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new MockModuleCatalog { Modules = { moduleInfo } };
            ModuleManager manager = new ModuleManager(loader, catalog);
            var moduleTypeLoader = new MockModuleTypeLoader();

            Exception retrieverException = new Exception();
            moduleTypeLoader.LoadCompletedError = retrieverException;

            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { moduleTypeLoader };
            Assert.False(loader.InitializeCalled);

            try
            {
                manager.Run();
            }
            catch (Exception ex)
            {
                Assert.IsType<ModuleTypeLoadingException>(ex);
                Assert.Equal(moduleInfo.ModuleName, ((ModularityException)ex).ModuleName);
                Assert.Contains(moduleInfo.ModuleName, ex.Message);
                Assert.Same(retrieverException, ex.InnerException);
                return;
            }

            //Assert.Fail("Exception not thrown.");
        }

        [Fact]
        public void ShouldThrowIfNoRetrieverCanRetrieveModule()
        {
            var ex = Assert.Throws<ModuleTypeLoaderNotFoundException>(() =>
            {

                var loader = new MockModuleInitializer();
                var catalog = new MockModuleCatalog { Modules = { CreateModuleInfo("ModuleThatNeedsRetrieval", InitializationMode.WhenAvailable) } };
                ModuleManager manager = new ModuleManager(loader, catalog)
                {
                    ModuleTypeLoaders = new List<IModuleTypeLoader> { new MockModuleTypeLoader() { canLoadModuleTypeReturnValue = false } }
                };
                manager.Run();
            });
        }

        [Fact]
        public void ShouldWorkIfModuleLoadsAnotherOnDemandModuleWhenInitializing()
        {
            var initializer = new StubModuleInitializer();
            var onDemandModule = CreateModuleInfo(typeof(MockModule), InitializationMode.OnDemand);
            onDemandModule.ModuleName = "OnDemandModule";
            var moduleThatLoadsOtherModule = CreateModuleInfo(typeof(MockModule), InitializationMode.WhenAvailable);
            var catalog = new MockModuleCatalog { Modules = { moduleThatLoadsOtherModule, onDemandModule } };
            ModuleManager manager = new ModuleManager(initializer, catalog);

            bool onDemandModuleWasInitialized = false;
            initializer.Initialize = m =>
            {
                if (m == moduleThatLoadsOtherModule)
                {
                    manager.LoadModule("OnDemandModule");
                }
                else if (m == onDemandModule)
                {
                    onDemandModuleWasInitialized = true;
                }
            };

            manager.Run();

            Assert.True(onDemandModuleWasInitialized);
        }

        [Fact]
        public void ModuleManagerIsDisposable()
        {
            Mock<IModuleInitializer> mockInit = new Mock<IModuleInitializer>();
            var moduleInfo = CreateModuleInfo("needsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new Mock<IModuleCatalog>();
            ModuleManager manager = new ModuleManager(mockInit.Object, catalog.Object);

            IDisposable disposableManager = manager as IDisposable;
            Assert.NotNull(disposableManager);
        }

        [Fact]
        public void DisposeDoesNotThrowWithNonDisposableTypeLoaders()
        {
            Mock<IModuleInitializer> mockInit = new Mock<IModuleInitializer>();
            var moduleInfo = CreateModuleInfo("needsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new Mock<IModuleCatalog>();
            ModuleManager manager = new ModuleManager(mockInit.Object, catalog.Object);

            var mockTypeLoader = new Mock<IModuleTypeLoader>();
            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { mockTypeLoader.Object };

            try
            {
                manager.Dispose();
            }
            catch (Exception)
            {
                //Assert.Fail();
            }
        }

        [Fact]
        public void DisposeCleansUpDisposableTypeLoaders()
        {
            Mock<IModuleInitializer> mockInit = new Mock<IModuleInitializer>();
            var moduleInfo = CreateModuleInfo("needsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new Mock<IModuleCatalog>();
            ModuleManager manager = new ModuleManager(mockInit.Object, catalog.Object);

            var mockTypeLoader = new Mock<IModuleTypeLoader>();
            var disposableMockTypeLoader = mockTypeLoader.As<IDisposable>();
            disposableMockTypeLoader.Setup(loader => loader.Dispose());

            manager.ModuleTypeLoaders = new List<IModuleTypeLoader> { mockTypeLoader.Object };

            manager.Dispose();

            disposableMockTypeLoader.Verify(loader => loader.Dispose(), Times.Once());
        }

        [Fact]
        public void DisposeDoesNotThrowWithMixedTypeLoaders()
        {
            Mock<IModuleInitializer> mockInit = new Mock<IModuleInitializer>();
            var moduleInfo = CreateModuleInfo("needsRetrieval", InitializationMode.WhenAvailable);
            var catalog = new Mock<IModuleCatalog>();
            ModuleManager manager = new ModuleManager(mockInit.Object, catalog.Object);

            var mockTypeLoader1 = new Mock<IModuleTypeLoader>();

            var mockTypeLoader = new Mock<IModuleTypeLoader>();
            var disposableMockTypeLoader = mockTypeLoader.As<IDisposable>();
            disposableMockTypeLoader.Setup(loader => loader.Dispose());

            manager.ModuleTypeLoaders = new List<IModuleTypeLoader>() { mockTypeLoader1.Object, mockTypeLoader.Object };

            try
            {
                manager.Dispose();
            }
            catch (Exception)
            {
                //Assert.Fail();
            }

            disposableMockTypeLoader.Verify(loader => loader.Dispose(), Times.Once());
        }
        private static ModuleInfo CreateModuleInfo(string name, InitializationMode initializationMode, params string[] dependsOn)
        {
            ModuleInfo moduleInfo = new ModuleInfo(name, name)
            {
                InitializationMode = initializationMode
            };
            moduleInfo.DependsOn.AddRange(dependsOn);
            return moduleInfo;
        }

        private static ModuleInfo CreateModuleInfo(Type type, InitializationMode initializationMode, params string[] dependsOn)
        {
            ModuleInfo moduleInfo = new ModuleInfo(type.Name, type.AssemblyQualifiedName)
            {
                InitializationMode = initializationMode
            };
            moduleInfo.DependsOn.AddRange(dependsOn);
            return moduleInfo;
        }
    }

    internal class MockModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockModuleCatalog : IModuleCatalog
    {
        public List<IModuleInfo> Modules = new List<IModuleInfo>();
        public Func<IModuleInfo, IEnumerable<IModuleInfo>> GetDependentModules;

        public Func<IEnumerable<IModuleInfo>, IEnumerable<IModuleInfo>> CompleteListWithDependencies;
        public Action ValidateCatalog;

        public void Initialize()
        {
            this.ValidateCatalog?.Invoke();
        }

        IEnumerable<IModuleInfo> IModuleCatalog.Modules => Modules;

        IEnumerable<IModuleInfo> IModuleCatalog.GetDependentModules(IModuleInfo moduleInfo)
        {
            if (GetDependentModules == null)
                return new List<IModuleInfo>();

            return GetDependentModules(moduleInfo);
        }

        IEnumerable<IModuleInfo> IModuleCatalog.CompleteListWithDependencies(IEnumerable<IModuleInfo> modules)
        {
            if (CompleteListWithDependencies != null)
                return CompleteListWithDependencies(modules);
            return modules;
        }

        public IModuleCatalog AddModule(IModuleInfo moduleInfo)
        {
            this.Modules.Add(moduleInfo);
            return this;
        }
    }

    internal class MockModuleInitializer : IModuleInitializer
    {
        public bool InitializeCalled;
        public List<IModuleInfo> InitializedModules = new List<IModuleInfo>();

        public void Initialize(IModuleInfo moduleInfo)
        {
            InitializeCalled = true;
            this.InitializedModules.Add(moduleInfo);
        }
    }

    internal class StubModuleInitializer : IModuleInitializer
    {
        public Action<ModuleInfo> Initialize;

        void IModuleInitializer.Initialize(IModuleInfo moduleInfo)
        {
            this.Initialize((ModuleInfo)moduleInfo);
        }
    }

    internal class MockDelegateModuleInitializer : IModuleInitializer
    {
        public Action<ModuleInfo> LoadBody;

        public void Initialize(IModuleInfo moduleInfo)
        {
            LoadBody((ModuleInfo)moduleInfo);
        }
    }
}
