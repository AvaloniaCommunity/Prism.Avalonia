using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace SampleDialogApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            // Since this is a basic ShellWindow, there's nothing
            // to do here.
            // For enterprise apps, you could register up subscriptions
            // or other startup background tasks so that they get triggered
            // on startup, rather than putting them in the DashboardViewModel.
            //
            // For example, initiate the pulling of News Feeds, etc.

            _dialogService = dialogService;

            Title = "My Dialog!";
        }

        public DelegateCommand CmdShowDialog => new DelegateCommand(() =>
        {
            var message = "This is a message that should be shown in the dialog.";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialogView", new DialogParameters($"message={message}"), r =>
            {
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
            _dialogService.Show("NotificationDialogView", r =>
            {
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
    }
}
