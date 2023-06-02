using SampleMvvmApp.Views;
using Prism.Commands;
using Prism.Regions;

namespace SampleMvvmApp.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        private const int Collapsed = 40;
        private const int Expanded = 200;

        private readonly IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;
        private int _flyoutWidth;

        public SidebarViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Navigation";
            FlyoutWidth = Expanded;
        }

        public DelegateCommand CmdDashboard => new(() =>
        {
            //// _journal.Clear();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DashboardView));
        });

        public DelegateCommand CmdFlyoutMenu => new(() =>
        {
            var isExpanded = FlyoutWidth == Expanded;
            FlyoutWidth = isExpanded ? Collapsed : Expanded;
        });

        public DelegateCommand CmdSettings => new(() => _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SettingsView)));

        public int FlyoutWidth
        {
            get => _flyoutWidth;
            set => SetProperty(ref _flyoutWidth, value);
        }
    }
}
