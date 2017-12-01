using System;
using System.Globalization;
using Prism.Avalonia.Infrastructure;
using Prism.Logging;

namespace ModulesSample.Module_System_Logic
{
    class ModuleTracker : IModuleTracker
    {
        private ILoggerFacade logger;

        public ModuleTracker(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this.logger = logger;
        }

        public ModuleTrackingState ModuleCodeEditorTrackingState { get; }

        public void RecordModuleLoaded(string moduleName)
        {
            this.logger.Log(string.Format(CultureInfo.CurrentCulture, "'{0}' module loaded.", moduleName),
                Category.Debug, Priority.Low);
        }

        public void RecordModuleConstructed(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture, "'{0}' module constructed.", moduleName),
                Category.Debug, Priority.Low);
        }

        public void RecordModuleInitialized(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
            }


            this.logger.Log(string.Format(CultureInfo.CurrentCulture, "'{0}' module initialized.", moduleName),
                Category.Debug, Priority.Low);
        }

        private ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            switch (moduleName)
            {
                default:
                    return null;
            }
        }
    }
}