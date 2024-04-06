using Prism.Regions;

namespace SampleMvvmApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            // Since this is a basic ShellWindow, there's nothing
            // to do here.
            // For enterprise apps, you could register up subscriptions
            // or other startup background tasks so that they get triggered
            // on startup, rather than putting them in the DashboardViewModel.
            //
            // For example, initiate the pulling of News Feeds, etc.

            Title = "Sample Prism.Avalonia MVVM!";
        }
    }
}
