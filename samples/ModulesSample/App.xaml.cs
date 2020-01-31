using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using ModulesSample.Module_System_Logic;
using Prism.Avalonia.Infrastructure;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Serilog;

namespace ModulesSample
{
    public class App : PrismApplication
    {
        public CallbackLogger CallbackLogger { get; } = new CallbackLogger();

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            InitializeLogging();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static void InitializeLogging()
        {
#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CallbackLogger);
            containerRegistry.RegisterSingleton<IModuleTracker, ModuleTracker>();
        }

        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(new ModuleInfo()
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = KnownModuleNames.ModuleDummy,
                ModuleType = typeof(DummyModule.DummyModule).AssemblyQualifiedName
            });
            moduleCatalog.AddModule(new ModuleInfo()
            {
                InitializationMode = InitializationMode.WhenAvailable,
                ModuleName = KnownModuleNames.ModuleDummy2,
                ModuleType = typeof(DummyModule2.DummyModule2).AssemblyQualifiedName
            });

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
