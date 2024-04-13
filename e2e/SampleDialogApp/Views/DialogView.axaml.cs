using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Prism.Ioc;
using Prism.Dialogs;
using SampleDialogApp.ViewModels;

namespace SampleDialogApp.Views;

public partial class DialogView : UserControl
{
    public DialogView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Pass the parent window to the ViewModel
        // given that the ViewModel has been binded to this view
        DialogViewModel? viewModel = this.DataContext as DialogViewModel;
        if (this.Parent is Window parent &&
            viewModel is not null)
        {
            viewModel.ParentWindow = parent;
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void BtnShowModal_Click(object sender, RoutedEventArgs args)
    {
        var dialogSvc = ContainerLocator.Current.Resolve<IDialogService>();

        var title = "MessageBox Title Here";
        var message = "Hello, I am a modal MessageBox called via the `.axaml.cs` UserControl.\n\n" +
                      "My parent window is the DialogView's window.";

        // Really you could just use 'this.Parent'. This ensures that our parent is a window and not another UserControl.
        var parentWindow = this.Parent is Window parent ? parent : null;

        dialogSvc.ShowDialog(
            nameof(MessageBoxView),
            new DialogParameters($"title={title}&message={message}")
            {
                // This informs the DialogService to use this window for MessageBoxView's parent.
                { KnownDialogParameters.ParentWindow, this.Parent },
            });

        // v8.1.97
        ////dialogSvc.ShowDialog(
        ////    this.Parent as Window,
        ////    nameof(MessageBoxView),
        ////    new DialogParameters($"title={title}&message={message}"),
        ////    r => { });
    }
}
