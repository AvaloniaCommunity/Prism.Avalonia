using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicMvvmApp.Views
{
    public partial class SidebarView : UserControl
    {
        public SidebarView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
