using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace SampleDialogApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private string _returnedResult;

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _dialogService = dialogService;

            Title = "My Dialog!";
        }

        public DelegateCommand CmdShowDialog => new DelegateCommand(() =>
        {
            var message = "This is a message that should be shown in the dialog.";

            _dialogService.ShowDialog("NotificationDialogView", new DialogParameters($"message={message}"), r =>
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
            _dialogService.Show("NotificationDialogView", r =>
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

        public string ReturnedResult
        {
            get => _returnedResult;
            set => SetProperty(ref _returnedResult, value);
        }
    }
}
