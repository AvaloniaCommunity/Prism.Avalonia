using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicMvvmApp.Views
{
    /// <summary>Sample Settings View.</summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
