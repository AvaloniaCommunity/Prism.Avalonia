using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Dialogs;

namespace SampleDialogApp.Views;

public partial class TestDialog : Window,IDialogWindow
{
    public TestDialog()
    {
        InitializeComponent();
    }

    public IDialogResult Result { get; set; }
}
