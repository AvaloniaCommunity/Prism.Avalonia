using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Prism.Interactivity.DefaultPopupWindows
{
    public class DefaultWindow : Window
    {
        public DefaultWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
