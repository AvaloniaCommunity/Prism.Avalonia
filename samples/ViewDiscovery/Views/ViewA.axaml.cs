using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ViewDiscovery.Views;

/// <summary>Interaction logic for ViewA.xaml</summary>
public partial class ViewA : UserControl
{
    public ViewA()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
