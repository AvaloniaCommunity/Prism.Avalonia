using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Dialogs;

namespace SampleDialogApp.Views;

/// <summary>Custom dialog window host.</summary>
public partial class CustomDialogWindow : Window, IDialogWindow
{
    public CustomDialogWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    /// <summary>The <see cref="IDialogResult"/> of the dialog.</summary>
    public IDialogResult Result { get; set; }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
