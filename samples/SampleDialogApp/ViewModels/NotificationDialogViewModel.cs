using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace SampleDialogApp.ViewModels
{
    public class NotificationDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IDialogService _dialog;
        private string _customMessage;
        private string _title = "Notification";

        public NotificationDialogViewModel()
        {
            // Since this is a basic ShellWindow, there's nothing
            // to do here.
            // For enterprise apps, you could register up subscriptions
            // or other startup background tasks so that they get triggered
            // on startup, rather than putting them in the DashboardViewModel.
            //
            // For example, initiate the pulling of News Feeds, etc.

            Title = "Sample Dialog!";
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

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
}
