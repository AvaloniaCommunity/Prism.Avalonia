using System;
using Avalonia.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;
using SampleDialogApp.Views;

namespace SampleDialogApp.ViewModels;

public class DialogViewModel : BindableBase, IDialogAware
{
    private readonly IDialogService _dialogService;
    private string _customMessage = string.Empty;
    private string _title = "Notification";
    private Window? _parentWindow = null;

    public DialogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        // This is a ViewModel of a pop-up dialog,
        // the Title is pre-binded to Prism.Avalonia's DialogService
        Title = "I'm a Sample Dialog!";
    }

    public DialogCloseListener RequestClose { get; }

    public string Title { get => _title; set => SetProperty(ref _title, value); }

    /////// <summary>Gets or sets the optional parent window of this Dialog pop-up.</summary>
    ////public Window? ParentWindow { get => _parentWindow; set => SetProperty(ref _parentWindow, value); }

    public DelegateCommand CmdModalDialog => new(() =>
    {
        var title = "MessageBox Title Here";
        var message = "Hello, I am a modal MessageBox window.";

        // v9.0.271
        _dialogService.ShowDialog(
            nameof(MessageBoxView),
            new DialogParameters($"title={title}&message={message}"));

        // 8.1.97
        ////_dialogService.ShowDialog(
        ////    ParentWindow,
        ////    nameof(MessageBoxView),
        ////    new DialogParameters($"title={title}&message={message}"));
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

        // NEW
        RequestClose.Invoke(result);

        // OLD
        ////RaiseRequestClose(new DialogResult(result));
    });

    public string CustomMessage { get => _customMessage; set => SetProperty(ref _customMessage, value); }

    public virtual bool CanCloseDialog()
    {
        // Allow the dialog to close
        return true;
    }

    public virtual void OnDialogClosed()
    {
        System.Diagnostics.Debug.WriteLine("Detach custom event handlers here, etc.");
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        CustomMessage = parameters.GetValue<string>("message");
    }

    ////public virtual void RaiseRequestClose(IDialogResult dialogResult)
    ////{
    ////    RequestClose?.Invoke(dialogResult);
    ////}
}
