using ModulesSample.Infrastructure;
using Prism.Modularity;
using Prism.Mvvm;

namespace ModulesSample.ModuleSystemLogic
{
    internal class ModuleTrackingState : BindableBase
    {
        private string _moduleName;
        private string _configuredDependencies = "(none)";
        private ModuleInitializationStatus _moduleInitializationStatus;
        private InitializationMode _expectedInitializationMode;
        private DiscoveryMethod _expectedDiscoveryMethod;

        public string ModuleName
        {
            get => _moduleName;
            set => SetProperty(ref _moduleName, value);
        }

        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get => _moduleInitializationStatus;
            set => SetProperty(ref _moduleInitializationStatus, value);
        }

        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get => _expectedDiscoveryMethod;
            set => SetProperty(ref _expectedDiscoveryMethod, value);
        }

        public InitializationMode ExpectedInitializationMode
        {
            get => _expectedInitializationMode;
            set => SetProperty(ref _expectedInitializationMode, value);
        }

        public string ConfiguredDependencies
        {
            get => _configuredDependencies;
            set => SetProperty(ref _configuredDependencies, value);
        }
    }
}
