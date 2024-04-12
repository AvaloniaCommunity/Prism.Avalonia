using SampleMvvmApp.Views;
using Prism.Commands;
using Prism.Navigation.Regions;
using Prism.Navigation;

namespace SampleMvvmApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public SettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Settings";
        }

        public DelegateCommand CmdNavigateToChild => new(() =>
        {
            var navParams = new NavigationParameters
            {
                { "key1", "Some text" },
                { "key2", 999 }
            };

            _regionManager.RequestNavigate(
                RegionNames.ContentRegion,
                nameof(SubSettingsView),
                navParams);
        });

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
    }
}
