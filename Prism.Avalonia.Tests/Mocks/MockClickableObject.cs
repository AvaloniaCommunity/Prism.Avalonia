using Avalonia.Controls;

namespace Prism.Avalonia.Tests.Mocks
{
    internal class MockClickableObject : Button
    {
        public void RaiseClick()
        {
            OnClick();
        }
    }
}