using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SampleDialogApp.Views;

namespace SampleDialogApp.ViewModels;

public class DialogViewModel : BindableBase, IDialogAware
{
    private readonly IDialogService _dialogService;
    private string _customMessage = string.Empty;
    private string _title = "Notification";

    public DialogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        Title = "Sample Dialog!";
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public DelegateCommand CmdModalDialog => new(() =>
    {
        var title = "MessageBox Title Here";
        var message = "Hello, I am a simple MessageBox modal window with an OK button.\n\n" +
                      "When too much text is added, a scrollbar will appear.";
        _dialogService.ShowDialog(nameof(MessageBoxView), new DialogParameters($"title={title}&message={message}"), r => { });

    });

    public DelegateCommand<string> CmdResult => new DelegateCommand<string>((param) =>
    {
        // None = 0
        // OK = 1
        // Cancel = 2
        // Abort = 3
        // Retry = 4
        // Ignore = 5
        // Yes = 6
        // No = 7
        ButtonResult result = ButtonResult.None;

        if (int.TryParse(param, out int intResult))
            result = (ButtonResult)intResult;

        RaiseRequestClose(new DialogResult(result));
    });

    public string CustomMessage
    {
        get => _customMessage;
        set => SetProperty(ref _customMessage, value);
    }

    public event Action<IDialogResult> RequestClose;

    public virtual bool CanCloseDialog()
    {
        // Allow the dialog to close
        return true;
    }

    public virtual void OnDialogClosed()
    {
        // Detatch custom eventhandlers here, etc.
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        CustomMessage = parameters.GetValue<string>("message");
    }

    public virtual void RaiseRequestClose(IDialogResult dialogResult)
    {
        RequestClose?.Invoke(dialogResult);
    }
}
