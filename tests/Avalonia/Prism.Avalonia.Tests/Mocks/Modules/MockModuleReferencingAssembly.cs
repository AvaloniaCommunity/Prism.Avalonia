using Prism.Ioc;
using Prism.Modularity;
using System;

namespace Prism.Avalonia.Tests.Mocks.Modules
{
    public class MockModuleReferencingAssembly : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            MockReferencedModule instance = new MockReferencedModule();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }
}