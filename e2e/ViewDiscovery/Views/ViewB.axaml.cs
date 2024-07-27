using Avalonia.Controls;

namespace ViewDiscovery.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewB : UserControl
    {
        public ViewB()
        {
            InitializeComponent();
        }

        public ViewB(ViewA subView)
        {
            InitializeComponent();
            this.FindControl<ContentControl>("Test").Content = subView;
        }
    }
}
