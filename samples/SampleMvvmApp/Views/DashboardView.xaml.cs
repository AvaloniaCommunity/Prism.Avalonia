using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SampleMvvmApp.Views
{
    /// <summary>DashboardView.</summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
