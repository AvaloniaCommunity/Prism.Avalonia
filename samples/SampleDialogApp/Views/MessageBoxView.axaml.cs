using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SampleDialogApp.Views;

public partial class MessageBoxView : UserControl
{
    public MessageBoxView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
