using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using SampleMvvmApp.ViewModels;
using SampleMvvmApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;

namespace SampleMvvmApp
{
    /// <summary>
    ///   Application entry point.
    ///   The methods in this file are layed out in their respective calling order
    ///   to help you learn the order of operation.
    /// </summary>
    public class App : PrismApplication
    {
        /// <summary>App entry point.</summary>
        public App()
        {
            Console.WriteLine("Constructor()");
        }

        // Note:
        //  Though, Prism.WPF v8.1 uses, `protected virtual void Initialize()`
        //  Avalonia's AppBuilderBase.cs calls, `.Setup() { ... Instance.Initialize(); ... }`
        //  Therefore, we need this as a `public override void` in PrismApplicationBase.cs
        public override void Initialize()
        {
            Console.WriteLine("Initialize()");
            AvaloniaXamlLoader.Load(this);

            // DON'T FORGET TO CALL THIS
            base.Initialize();
        }

        /// <summary>Called after Initialize.</summary>
        protected override void OnInitialized()
        {
            // Register Views to the Region it will appear in. Don't register them in the ViewModel.
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
            regionManager.RegisterViewWithRegion(RegionNames.SidebarRegion, typeof(SidebarView));

            ////var logService = Container.Resolve<ILogService>();
            ////logService.Configure("swlog.config");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Console.WriteLine("RegisterTypes()");

            // Services
            //// containerRegistry.RegisterSingleton<ILogService, LogService>();

            // Views - Generic
            containerRegistry.Register<SidebarView>();
            containerRegistry.Register<MainWindow>();

            // Views - Region Navigation
            containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SubSettingsView, SubSettingsViewModel>();
        }

        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override IAvaloniaObject CreateShell()
        {
            Console.WriteLine("CreateShell()");
            return Container.Resolve<MainWindow>();
        }
    }
}
