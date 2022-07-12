using Avalonia;
using Avalonia.Controls;
using System.Windows;

namespace Prism.Avalonia.Tests.Mocks
{
    public class MockFrameworkElement : Control
    {
        public void RaiseLoaded()
        {
            RaiseLoaded();
        }

        public void RaiseUnloaded()
        {
            RaiseUnloaded();
        }
    }
}
