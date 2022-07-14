using System;
using System.Collections.Generic;
using System.Text;

namespace Prism.Container.Avalonia.Mocks
{
    internal partial class NullModuleCatalogBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return null;
        }

        protected override DependencyObject CreateShell()
        {
            return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
