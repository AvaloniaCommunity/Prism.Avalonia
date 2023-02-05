using Prism.Commands;
using Prism.Services.Dialogs;
using SampleDialogApp.Views;

namespace SampleDialogApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private string _returnedResult = "";

    public MainWindowViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        Title = "My Dialog!";
    }

    public DelegateCommand CmdShowMsgBox => new(() =>
    {
        var title = "MessageBox Title Here";
        var message = "Hello, I am a simple MessageBox modal window with an OK button.\n\n" +
                      "When too much text is added, a scrollbar will appear.";
        _dialogService.ShowDialog(nameof(MessageBoxView), new DialogParameters($"title={title}&message={message}"), r => { });
    });

    public DelegateCommand CmdShowDialog => new DelegateCommand(() =>
    {
        var message = "This is a message that should be shown in the dialog.";

        // PRO TIP: Use `nameof(DialogView)` instead of "DialogView" to catch errors early on
        _dialogService.ShowDialog(nameof(DialogView), new DialogParameters($"message={message}"), r =>
        {
            if (r is null)
            {
                Title = "Try again";
                ReturnedResult = "Null result returned";
            }

            ReturnedResult = r.Result.ToString();

            if (r.Result == ButtonResult.None)
                Title = "Result is None";
            else if (r.Result == ButtonResult.OK)
                Title = "Result is OK";
            else if (r.Result == ButtonResult.Cancel)
                Title = "Result is Cancel";
            else
                Title = "I Don't know what you did!?";
        });
    });

    public DelegateCommand CmdShowRegular => new DelegateCommand(() =>
    {
        _dialogService.Show(nameof(DialogView), r =>
        {
            if (r is null)
            {
                Title = "Try again";
                ReturnedResult = "Null result returned";
            }

            ReturnedResult = r.Result.ToString();

            // Same as if-statements, just a switch-expression.
            Title = r.Result switch
            {
                ButtonResult.None => "Result is None",
                ButtonResult.OK => "Result is OK",
                ButtonResult.Cancel => "Result is Cancel",
                _ => "I Don't know what you did!?",
            };
        });
    });

    public string ReturnedResult
    {
        get => _returnedResult;
        set => SetProperty(ref _returnedResult, value);
    }
}
