

using System.Windows.Controls.Primitives;

namespace Prism.Avalonia.Tests.Mocks
{
    internal class MockClickableObject : ButtonBase
    {
        public void RaiseClick()
        {
            OnClick();
        }
    }
}