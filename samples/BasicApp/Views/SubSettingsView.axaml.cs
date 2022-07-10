using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicMvvmApp.Views
{
    public partial class SubSettingsView : UserControl
    {
        public SubSettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
