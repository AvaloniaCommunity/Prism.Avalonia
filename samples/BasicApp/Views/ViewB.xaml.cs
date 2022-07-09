using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicApp.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewB : UserControl
    {
        public ViewB() { }
        public ViewB(ViewA subView)
        {
            this.InitializeComponent();
            //// this.FindControl<ContentControl>("Test").Content = subView;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
