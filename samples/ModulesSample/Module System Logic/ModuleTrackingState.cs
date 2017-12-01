using Prism.Avalonia.Infrastructure;
using Prism.Modularity;
using Prism.Mvvm;

namespace ModulesSample.Module_System_Logic
{
    class ModuleTrackingState : BindableBase
    {
        private string moduleName;
        private string configuredDependencies = "(none)";
        private ModuleInitializationStatus moduleInitializationStatus;
        private InitializationMode expectedInitializationMode;
        private DiscoveryMethod expectedDiscoveryMethod;

        public string ModuleName
        {
            get { return this.moduleName; }
            set
            {
                if (this.moduleName != value)
                {
                    base.SetProperty(ref this.moduleName, value);
                }
            }
        }

        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get { return this.moduleInitializationStatus; }
            set
            {
                if (this.moduleInitializationStatus != value)
                {
                    base.SetProperty(ref this.moduleInitializationStatus, value);
                }
            }
        }

        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get { return this.expectedDiscoveryMethod; }
            set
            {
                if (this.expectedDiscoveryMethod != value)
                {
                    base.SetProperty(ref this.expectedDiscoveryMethod, value);
                }
            }
        }

        public InitializationMode ExpectedInitializationMode
        {
            get { return this.expectedInitializationMode; }
            set
            {
                if (this.expectedInitializationMode != value)
                {
                    base.SetProperty(ref this.expectedInitializationMode, value);
                }
            }
        }

        public string ConfiguredDependencies
        {
            get { return this.configuredDependencies; }
            set
            {
                if (this.configuredDependencies != value)
                {
                    base.SetProperty(ref this.configuredDependencies, value);
                }
            }
        }
    }
}