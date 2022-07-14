using System;
using System.Collections.Generic;
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Resolution;
using Unity.Extension;
using Unity.Registration;
using Unity.Lifetime;
using Unity.Injection;

namespace Prism.Unity.Avalonia.Tests
{
    [TestClass]
    public class UnityServiceLocatorAdapterFixture
    {
        [TestMethod]
        public void ShouldForwardResolveToInnerContainer()
        {
            object myInstance = new object();

            IUnityContainer container = new MockUnityContainer()
                                            {
                                                ResolveMethod = delegate
                                                                    {
                                                                        return myInstance;
                                                                    }
                                            };

            IServiceLocator containerAdapter = new UnityServiceLocatorAdapter(container);

            Assert.IsTrue(myInstance == containerAdapter.GetInstance(typeof (object)));

        }

        [TestMethod]
        public void ShouldForwardResolveAllToInnerContainer()
        {
            IEnumerable<object> list = new List<object> {new object(), new object()};

            IUnityContainer container = new MockUnityContainer()
            {
                ResolveMethod = delegate
                {
                    return list;
                }
            };

            IServiceLocator containerAdapter = new UnityServiceLocatorAdapter(container);

            Assert.IsTrue(list == containerAdapter.GetAllInstances(typeof (object)));
        }

        private class MockUnityContainer : IUnityContainer
        {
            public Func<object> ResolveMethod { get; set; }

            #region Implementation of IDisposable

            public void Dispose()
            {

            }

            #endregion

            #region Implementation of IUnityContainer

            public IUnityContainer Parent => throw new NotImplementedException();

            public IEnumerable<IContainerRegistration> Registrations => throw new NotImplementedException();

            public IUnityContainer AddExtension(UnityContainerExtension extension)
            {
                throw new NotImplementedException();
            }

            public object BuildUp(Type type, object existing, string name, params ResolverOverride[] resolverOverrides)
            {
                throw new NotImplementedException();
            }

            public object Configure(Type configurationInterface)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer CreateChildContainer()
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RegisterInstance(Type type, string name, object instance, LifetimeManager lifetime)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RegisterType(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RemoveAllExtensions()
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides)
            {
                return ResolveMethod();
            }

            public bool IsRegistered(Type type, string name)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RegisterType(Type registeredType, Type mappedToType, string name, ITypeLifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RegisterInstance(Type type, string name, object instance, IInstanceLifetimeManager lifetimeManager)
            {
                throw new NotImplementedException();
            }

            public IUnityContainer RegisterFactory(Type type, string name, Func<IUnityContainer, Type, string, object> factory, IFactoryLifetimeManager lifetimeManager)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}