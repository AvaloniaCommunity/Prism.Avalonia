using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Prism.IocContainer.Avalonia.Tests.Support.Mocks;

namespace Prism.Unity.Avalonia.Tests
{
    [TestClass]
    public class UnityContainerExtensionFixture
    {
        [TestMethod]
        public void ExtensionReturnsTrueIfThereIsAPolicyForType()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<object, string>();
            Assert.IsTrue(container.IsTypeRegistered(typeof(object)));
            Assert.IsFalse(container.IsTypeRegistered(typeof(int)));

            container.RegisterType<IList<int>, List<int>>();

            Assert.IsTrue(container.IsTypeRegistered(typeof(IList<int>)));
            Assert.IsFalse(container.IsTypeRegistered(typeof(IList<string>)));

            container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>));
            Assert.IsTrue(container.IsTypeRegistered(typeof(IDictionary<,>)));
        }

        [TestMethod]
        public void TryResolveShouldResolveTheElementIfElementExist()
        {
            var container = new UnityContainer();
            container.RegisterType<IService, MockService>();

            object dependantA = container.TryResolve<IService>();
            Assert.IsNotNull(dependantA);
        }

        [TestMethod]
        public void TryResolveShouldReturnNullIfElementNotExist()
        {
            var container = new UnityContainer();

            object dependantA = container.TryResolve<IDependantA>();
            Assert.IsNull(dependantA);
        }

        [TestMethod]
        public void TryResolveWorksWithValueTypes()
        {
            var container = new UnityContainer();

            int valueType = container.TryResolve<int>();
            Assert.IsTrue(default(int) == valueType);
        }

    }
}