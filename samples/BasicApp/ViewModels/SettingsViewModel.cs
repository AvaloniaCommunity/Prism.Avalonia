using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BasicMvvmApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(IRegionManager regionManager)
        {
        }

        public DelegateCommand CmdNavigateToChild => new DelegateCommand(() =>
        {
            ;
        });
    }
}
