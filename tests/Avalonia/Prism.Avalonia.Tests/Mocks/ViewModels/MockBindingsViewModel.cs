using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace Prism.Avalonia.Tests.Mocks.ViewModels
{
    internal class MockBindingsViewModel : BindableBase
    {
        private int _mockProperty;
        private int _listItemSelected;
        private string _listItemText = string.Empty;
        private ObservableCollection<string> _listItems = new();

        public int MockProperty
        {
            get => _mockProperty;
            set => SetProperty(ref _mockProperty, value);
        }

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
