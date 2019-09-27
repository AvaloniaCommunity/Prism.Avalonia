using System.Linq;
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.IocContainer.Avalonia.Tests.Support.Mocks.Views;
using Unity;
using Prism.Regions;
using Prism.Unity.Avalonia.Tests.Mocks;

namespace Prism.Unity.Avalonia.Tests
{
    [TestClass]
    public class UnityRegionNavigationContentLoaderFixture
    {
        IUnityContainer _container;

        public UnityRegionNavigationContentLoaderFixture()
        {
            _container = new UnityContainer();
            MockServiceLocator serviceLocator = new MockServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        [TestMethod]
        public void ShouldFindCandidateViewInRegion()
        {
            _container.RegisterType<object, MockView>("MockView");

            // We cannot access the UnityRegionNavigationContentLoader directly so we need to call its
            // GetCandidatesFromRegion method through a navigation request.
            IRegion testRegion = new Region();

            MockView view = new MockView();
            testRegion.Add(view);
            testRegion.Deactivate(view);

            testRegion.RequestNavigate("MockView");

            Assert.IsTrue(testRegion.Views.Contains(view));
            Assert.IsTrue(testRegion.Views.Count() == 1);
            Assert.IsTrue(testRegion.ActiveViews.Count() == 1);
            Assert.IsTrue(testRegion.ActiveViews.Contains(view));
        }

        [TestMethod]
        public void ShouldFindCandidateViewWithFriendlyNameInRegion()
        {
            _container.RegisterType<object, MockView>("SomeView");

            // We cannot access the UnityRegionNavigationContentLoader directly so we need to call its
            // GetCandidatesFromRegion method through a navigation request.
            IRegion testRegion = new Region();

            MockView view = new MockView();
            testRegion.Add(view);
            testRegion.Deactivate(view);

            testRegion.RequestNavigate("SomeView");

            Assert.IsTrue(testRegion.Views.Contains(view));
            Assert.IsTrue(testRegion.ActiveViews.Count() == 1);
            Assert.IsTrue(testRegion.ActiveViews.Contains(view));
        }
    }
}
