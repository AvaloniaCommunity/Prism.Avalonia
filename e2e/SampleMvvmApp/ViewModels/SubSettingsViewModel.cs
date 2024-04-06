using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleMvvmApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace SampleMvvmApp.ViewModels
{
    public class SubSettingsViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal? _journal;
        private string _messageText = string.Empty;
        private string _messageNumber = string.Empty;

        public SubSettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Title = "Settings - SubView";
        }

        public DelegateCommand CmdNavigateBack => new DelegateCommand(() =>
        {
            // Go back to the previous calling page, otherwise, Dashboard.
            if (_journal != null && _journal.CanGoBack)
                _journal.GoBack();
            else
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DashboardView));
        });

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        public string MessageNumber
        {
            get => _messageNumber;
            set => SetProperty(ref _messageNumber, value);
        }

        /// <summary>Navigation completed successfully.</summary>
        /// <param name="navigationContext">Navigation context.</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Used to "Go Back" to parent
            _journal = navigationContext.NavigationService.Journal;

            // Display our parameters
            MessageText = navigationContext.Parameters["key1"].ToString();
            MessageNumber = navigationContext.Parameters["key2"].ToString();
        }

        public override bool OnNavigatingTo(NavigationContext navigationContext)
        {
            // Navigation permission sample:
            // Don't allow navigation if our keys are missing
            return navigationContext.Parameters.ContainsKey("key1") &&
                   navigationContext.Parameters.ContainsKey("key2");
        }
    }
}
