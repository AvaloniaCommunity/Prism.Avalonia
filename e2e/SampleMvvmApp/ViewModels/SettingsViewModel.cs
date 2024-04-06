using SampleMvvmApp.Views;
using Prism.Commands;
using Prism.Regions;

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

        public DelegateCommand CmdNavigateToChild => new DelegateCommand(() =>
        {
            var navParams = new NavigationParameters();
            navParams.Add("key1", "Some text");
            navParams.Add("key2", 999);

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
