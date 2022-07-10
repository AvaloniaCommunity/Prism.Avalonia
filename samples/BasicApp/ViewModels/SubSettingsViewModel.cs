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
    public class SubSettingsViewModel : ViewModelBase
    {
        public SubSettingsViewModel(IRegionManager regionManager)
        {
        }

        public DelegateCommand CmdNavigateBack => new DelegateCommand(() =>
        {
            ;
        });
    }
}
