using Avalonia;
using Avalonia.Controls;
using DryIoc;
using ModulesSample.Module_System_Logic;
using Prism.Avalonia.Infrastructure;
using Prism.DryIoc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace ModulesSample
{
    public class Bootstrapper : DryIocBootstrapper
    {
        private AppBuilder AppBuilderInstance;
        private readonly CallbackLogger callbackLogger = new CallbackLogger();

        public CallbackLogger CallbackLogger
        {
            get { return callbackLogger; }
        }

        /// <summary>
        /// Create the shell of the application
        /// </summary>
        /// <returns>The shell of application</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject" />, the 
        /// <see cref="Bootstrapper" /> will attach the default <seealso cref="Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <seealso cref="Prism.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML
        /// </remarks>
        protected override IAvaloniaObject CreateShell()
        {
            AppBuilderInstance = App.BuildAvaloniaApp();

            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// Initializes the shell
        /// </summary>
        /// <remarks>
        /// The base implementation ensures the shell is composed in the container
        /// </remarks>
        protected override void InitializeShell()
        {
            AppBuilderInstance.Start(base.Shell as Window);
        }

        protected override ILoggerFacade CreateLogger()
        {
            return this.CallbackLogger;
        }

        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add 
        /// type mappings required by the application
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            base.Container.UseInstance<CallbackLogger>(this.CallbackLogger);
            base.RegisterTypeIfMissing<IModuleTracker, ModuleTracker>(true);
        }

        /// <summary>
        /// Returns the module catalog that will be used to initialize the modules
        /// </summary>
        /// <returns>
        /// An instance of<see cref="IModuleCatalog"/> that will be used to initialize the modules
        /// </returns>
        /// <remarks>
        /// When using the default initialization behaviour, this method must be overwritten by a derived class
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = KnownModuleNames.ModuleDummy,
                ModuleType = typeof(DummyModule.DummyModule).AssemblyQualifiedName
            });
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = KnownModuleNames.ModuleDummy2,
                ModuleType = typeof(DummyModule2.DummyModule2).AssemblyQualifiedName
            });
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            return base.ConfigureRegionAdapterMappings();
        }
    }
}