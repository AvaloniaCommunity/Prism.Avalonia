using System;
using System.Linq.Expressions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace SampleMvvmApp.ViewModels
{

    public class ViewModelBase : BindableBase, INavigationAware
    {
        private string _title;

        // Provide wire-ups to NavigationService (aka: RegionManager).
        //
        ////private string _baseNavPage = string.Empty;
        ////private readonly IRegionManager _regionManager;
        ////private IRegionNavigationJournal? _journal;
        ////
        ////public ViewModelBase()
        ////{
        ////}
        ////
        ////public ViewModelBase(IRegionManager region, string baseNavPage = "")
        ////{
        ////    _baseNavPage = baseNavPage;
        ////}
        ////
        ////public IRegionManager Navigation => _regionManager;
        ////
        ////public virtual DelegateCommand CmdNavigateBack => new DelegateCommand(() =>
        ////{
        ////    // Go back to the previous calling page, otherwise, Dashboard.
        ////    if (_journal != null && _journal.CanGoBack)
        ////        _journal.GoBack();
        ////    else
        ////        _regionManager.RequestNavigate(RegionNames.ContentRegion, _baseNavPage); // nameof(DashboardView));
        ////});

        /// <summary>Gets or sets the title of the View.</summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        ///   Called to determine if this instance can handle the navigation request.
        ///   Don't call this directly, use <seealso cref="OnNavigatingTo(NavigationContext)"/>.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns><see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // Auto-allow navigation
            return OnNavigatingTo(navigationContext);
        }

        /// <summary>Called when the implementer is being navigated away from.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>Called when the implementer has been navigated to.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>Navigation validation checker.</summary>
        /// <remarks>Override for Prism 7.2's IsNavigationTarget.</remarks>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns><see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.</returns>
        public virtual bool OnNavigatingTo(NavigationContext navigationContext)
        {
            return true;
        }
    }
}
