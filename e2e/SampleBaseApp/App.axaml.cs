using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using SampleBaseApp.ViewModels;
using SampleBaseApp.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace SampleBaseApp;

public partial class App : PrismApplication
{
  public override void Initialize()
  {
    Console.WriteLine("Initialize()");

    AvaloniaXamlLoader.Load(this);

    // Required to initialize Prism.Avalonia - DO NOT REMOVE
    base.Initialize();
  }

  protected override AvaloniaObject CreateShell()
  {
    Console.WriteLine("CreateShell()");

    return Container.Resolve<MainWindow>();
  }

  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    // Add Services and ViewModel registrations here

    Console.WriteLine("RegisterTypes()");

    // -=[ Sample ]=-
    //
    //  // Services
    //  containerRegistry.RegisterSingleton<INotificationService, NotificationService>();
    //
    //  // Views - Region Navigation
    //  containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
  }

  /**
   * NOT NEEDED:
   *  The following is used by vanilla Avalonia. Prism.Avalonia only needs, `CreateShell()`
   *  
  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      // Line below is needed to remove Avalonia data validation.
      // Without this line you will get duplicate validations from both Avalonia and CT
      BindingPlugins.DataValidators.RemoveAt(0);
      desktop.MainWindow = new MainWindow
      {
        DataContext = new MainWindowViewModel(),
      };
    }

    base.OnFrameworkInitializationCompleted();
  }
  */
}
