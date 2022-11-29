using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Prism.Avalonia.Tests.Mocks.Views
{
    public partial class MockBindingsView : UserControl
    {
        public MockBindingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
