using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Avalonia.Tests.Mocks;

namespace Prism.Avalonia.Tests.Modularity
{
    /// <summary>
    /// Summary description for ModuleInitializerFixture
    /// </summary>
    [TestClass]
    public class ModuleInitializerFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullContainerThrows()
        {
            ModuleInitializer loader = new ModuleInitializer(null, new MockLogger());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullLoggerThrows()
        {
            ModuleInitializer loader = new ModuleInitializer(new MockContainerAdapter(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ModuleInitializeException))]
        public void InitializationExceptionsAreWrapped()
        {
            var moduleInfo = CreateModuleInfo(typeof(ExceptionThrowingModule));

            ModuleInitializer loader = new ModuleInitializer(new MockContainerAdapter(), new MockLogger());

            loader.Initialize(moduleInfo);
        }


        [TestMethod]
        public void ShouldResolveModuleAndInitializeSingleModule()
        {
            IContainerExtension containerFacade = new MockContainerAdapter();
            var service = new ModuleInitializer(containerFacade, new MockLogger());
            FirstTestModule.wasInitializedOnce = false;
            var info = CreateModuleInfo(typeof(FirstTestModule));
            service.Initialize(info);
            Assert.IsTrue(FirstTestModule.wasInitializedOnce);
        }


        [TestMethod]
        public void ShouldLogModuleInitializeErrorsAndContinueLoading()
        {
            IContainerExtension containerFacade = new MockContainerAdapter();
            var logger = new MockLogger();
            var service = new CustomModuleInitializerService(containerFacade, logger);
            var invalidModule = CreateModuleInfo(typeof(InvalidModule));

            Assert.IsFalse(service.HandleModuleInitializerrorCalled);
            service.Initialize(invalidModule);
            Assert.IsTrue(service.HandleModuleInitializerrorCalled);
        }

        [TestMethod]
        public void ShouldLogModuleInitializationError()
        {
            IContainerExtension containerFacade = new MockContainerAdapter();
            var logger = new MockLogger();
            var service = new ModuleInitializer(containerFacade, logger);
            ExceptionThrowingModule.wasInitializedOnce = false;
            var exceptionModule = CreateModuleInfo(typeof(ExceptionThrowingModule));

            try
            {
                service.Initialize(exceptionModule);
            }
            catch (ModuleInitializeException)
            {
            }

            Assert.IsNotNull(logger.LastMessage);
            StringAssert.Contains(logger.LastMessage, "ExceptionThrowingModule");
        }

        [TestMethod]
        public void ShouldThrowExceptionIfBogusType()
        {
            var moduleInfo = new ModuleInfo("TestModule", "BadAssembly.BadType");

            ModuleInitializer loader = new ModuleInitializer(new MockContainerAdapter(), new MockLogger());

            try
            {
                loader.Initialize(moduleInfo);
                Assert.Fail("Did not throw exception");
            }
            catch (ModuleInitializeException ex)
            {
                StringAssert.Contains(ex.Message, "BadAssembly.BadType");
            }
            catch(Exception)
            {
                Assert.Fail();
            }

        }

        private static ModuleInfo CreateModuleInfo(Type type, params string[] dependsOn)
        {
            ModuleInfo moduleInfo = new ModuleInfo(type.Name, type.AssemblyQualifiedName);
            moduleInfo.DependsOn.AddRange(dependsOn);
            return moduleInfo;
        }

        public static class ModuleLoadTracker
        {
            public static readonly Stack<Type> ModuleLoadStack = new Stack<Type>();
        }

        public class FirstTestModule : IModule
        {
            public static bool wasInitializedOnce;

            public void OnInitialized(IContainerProvider containerProvider)
            {
                wasInitializedOnce = true;
                ModuleLoadTracker.ModuleLoadStack.Push(GetType());
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }

        public class SecondTestModule : IModule
        {
            public static bool wasInitializedOnce;
            public static long initializedOnTickCount;

            public void OnInitialized(IContainerProvider containerProvider)
            {
                wasInitializedOnce = true;
                ModuleLoadTracker.ModuleLoadStack.Push(GetType());
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }

        public class DependantModule : IModule
        {
            public static bool wasInitializedOnce;

            public void OnInitialized(IContainerProvider containerProvider)
            {
                wasInitializedOnce = true;
                ModuleLoadTracker.ModuleLoadStack.Push(GetType());
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }

        public class DependencyModule : IModule
        {
            public static bool wasInitializedOnce;
            public static long initializedOnTickCount;

            public void OnInitialized(IContainerProvider containerProvider)
            {
                wasInitializedOnce = true;
                ModuleLoadTracker.ModuleLoadStack.Push(GetType());
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }

        public class ExceptionThrowingModule : IModule
        {
            public static bool wasInitializedOnce;
            public static long initializedOnTickCount;

            public void OnInitialized(IContainerProvider containerProvider)
            {
                throw new InvalidOperationException("Intialization can't be performed");
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                throw new NotImplementedException();
            }
        }

        public class InvalidModule { }

        public class CustomModuleInitializerService : ModuleInitializer
        {
            public bool HandleModuleInitializerrorCalled;

            public CustomModuleInitializerService(IContainerExtension containerFacade, ILoggerFacade logger)
                : base(containerFacade, logger)
            {
            }

            public override void HandleModuleInitializationError(IModuleInfo moduleInfo, string assemblyName, Exception exception)
            {
                HandleModuleInitializerrorCalled = true;
            }
        }

        public class Module1 : IModule
        {
            void IModule.OnInitialized(IContainerProvider containerProvider) { }
            void IModule.RegisterTypes(IContainerRegistry containerRegistry) { }
        }
        public class Module2 : IModule
        {
            void IModule.OnInitialized(IContainerProvider containerProvider) { }
            void IModule.RegisterTypes(IContainerRegistry containerRegistry) { }
        }
        public class Module3 : IModule
        {
            void IModule.OnInitialized(IContainerProvider containerProvider) { }
            void IModule.RegisterTypes(IContainerRegistry containerRegistry) { }
        }
        public class Module4 : IModule
        {
            void IModule.OnInitialized(IContainerProvider containerProvider) { }
            void IModule.RegisterTypes(IContainerRegistry containerRegistry) { }
        }
    }
}