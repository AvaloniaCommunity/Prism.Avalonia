using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Regions;
using SampleMvvmApp.Services;

namespace SampleMvvmApp.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly INotificationService _notification;
        private int _counter = 0;
        private int _listItemSelected = -1;
        private ObservableCollection<string> _listItems = new();
        private string _listItemText;

        public DashboardViewModel(IRegionManager regionManager, INotificationService notifyService)
        {
            _notification = notifyService;
        }

        public DelegateCommand CmdAddItem => new(() =>
        {
            _counter++;
            ListItems.Add($"Item Number: {_counter}");

            // Optionally use, `Insert(0, ..)` to insert items at the top
            //ListItems.Insert(0, entry);
        });

        public DelegateCommand CmdClearItems => new(() =>
        {
            ListItems.Clear();
        });

        public DelegateCommand CmdNotification => new(() =>
        {
            _notification.Show("Hello Prism!", "Notification Pop-up Message.");

            // Alternate OnClick action
            ////_notification.Show("Hello Prism!", "Notification Pop-up Message.", () =>
            ////{
            ////    // Action to perform
            ////});
        });

        public int ListItemSelected
        {
            get => _listItemSelected;
            set
            {
                SetProperty(ref _listItemSelected, value);

                if (value == -1)
                    return;

                ListItemText = ListItems[ListItemSelected];
            }
        }

        public string ListItemText
        {
            get => _listItemText;
            set => SetProperty(ref _listItemText, value);
        }

        public ObservableCollection<string> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
    }
}
