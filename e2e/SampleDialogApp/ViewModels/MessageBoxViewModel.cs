using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace SampleDialogApp.ViewModels;

/// <summary>
///     Simple Message Box Dialog ViewModel that only supports an OK button.
///
///     Parameter Inputs:
///         Title (string): Window's caption
///         Message (string): Text to display
/// </summary>
public class MessageBoxViewModel : BindableBase, IDialogAware
{
    private string _customMessage = string.Empty;
    private string _title = "Notification";
    private int _maxHeight;
    private int _maxWidth;

    public MessageBoxViewModel()
    {
        Title = "Alert!";

        MaxHeight = 800;
        MaxWidth = 600;
    }

    public event Action<IDialogResult>? RequestClose;

    public string Title { get => _title; set => SetProperty(ref _title, value); }

    public int MaxHeight { get => _maxHeight; set => SetProperty(ref _maxHeight, value); }

    public int MaxWidth { get => _maxWidth; set => SetProperty(ref _maxWidth, value); }

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

    public string CustomMessage { get => _customMessage; set => SetProperty(ref _customMessage, value); }

    public virtual bool CanCloseDialog()
    {
        // Allow the dialog to close
        return true;
    }

    public virtual void OnDialogClosed()
    {
        // Detach custom event handlers here, etc.
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        var title = parameters.GetValue<string>("title");
        if (!string.IsNullOrEmpty(title))
            Title = title;

        CustomMessage = parameters.GetValue<string>("message");
    }

    public virtual void RaiseRequestClose(IDialogResult dialogResult)
    {
        RequestClose?.Invoke(dialogResult);
    }
}
