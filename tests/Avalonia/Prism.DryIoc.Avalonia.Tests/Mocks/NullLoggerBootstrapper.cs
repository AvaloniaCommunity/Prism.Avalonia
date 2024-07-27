using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;

namespace Prism.Container.Avalonia.Mocks
{
    internal partial class NullLoggerBootstrapper : PrismBootstrapper
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new System.NotImplementedException();
        }

        protected override DependencyObject CreateShell()
        {
            throw new System.NotImplementedException();
        }
    }
}
