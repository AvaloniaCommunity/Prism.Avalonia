using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using SampleSplashScreen.ViewModels;
using SampleSplashScreen.Views;

namespace SampleSplashScreen;

public partial class App : PrismApplication
{
  public override void Initialize()
  {
    // Performs wire-ups
    Debug.WriteLine("1 - Initialize()");

    base.Initialize();
  }

  /// <summary>Register Services and Views.</summary>
  /// <param name="containerRegistry"></param>
  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    Debug.WriteLine("2 - RegisterTypes()");
  }

  protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
  {
    Debug.WriteLine("3 - ConfigureRegionAdapterMappings()");

    base.ConfigureRegionAdapterMappings(regionAdapterMappings);
  }

  protected override AvaloniaObject CreateShell()
  {
    Debug.WriteLine("4 - CreateShell()");

    // Set our starting window as the Splash-screen Window
    return Container.Resolve<SplashWindow>();
  }

  protected override void InitializeModules()
  {
    Debug.WriteLine("5 - InitializeModules()");

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
    {
      // Doing this method will result in, "Dispatcher Shutdown" error because
      // the window was shutdown.

      //var splash = new SplashWindow();
      //splash.Show();

      try
      {
        base.InitializeModules();
        //// System.Threading.Tasks.Task.Run(() => base.InitializeModules());
      }
      finally
      {
        //splash.Close();
      }
    }
    else
    {
      base.InitializeModules();
    }
  }

  /// <summary>Called after Initialize.</summary>
  protected override void OnInitialized()
  {
    Debug.WriteLine("6 - OnInitialized() - This shows the window set by, CreateShell() and InitializeShell()");

    // Executes, MainWindow?.Show();
    base.OnInitialized();
  }

  /// <summary>Framework initialization completed. Set the main window.</summary>
  public override async void OnFrameworkInitializationCompleted()
  {
    Debug.WriteLine("7 - OnFrameworkInitializationCompleted()");

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
    {
      // Grab current window (splash screen) to close it later.
      var splashInstance = this.MainWindow;

      // Perform splash screen initialization stuff here
      await OnSplashScreenAsync();

      // ---------------------------
      // Resolve the new main window
      // Similar to `Initialize()`, below is a candidate for a Prism method
      // to handle the switch-over on Desktop.
      var newShell = (AvaloniaObject)Container.Resolve<MainWindow>();

      if (newShell is not null)
      {
        // AutoWireViewModel is 'internal static' so we can't access it here
        // The following is the same as, "MvvmHelpers.AutowireViewModel(newShell);"
        if (newShell is Control view &&
            view.DataContext is null &&
            ViewModelLocator.GetAutoWireViewModel(view) is null)
        {
          ViewModelLocator.SetAutoWireViewModel(view, true);
        }

        RegionManager.SetRegionManager(newShell, Container.Resolve<IRegionManager>());
        RegionManager.UpdateRegions();

        // Set the new Shell window in Prism to MainWindow
        InitializeShell(newShell);
      }

      // Show (new) main window
      base.OnInitialized();

      // WARNING:
      // This MUST be performed after showing the new window.
      // Otherwise, your program will exit.
      (splashInstance as Window)?.Close();
    }
    else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
    {
      // Needs 
      singleViewLifetime.MainView = MainWindow as Control;
      base.OnFrameworkInitializationCompleted();
    }
    else
    {
      base.OnFrameworkInitializationCompleted();
    }
  }

  /// <summary>User-defined splash screen stuff.</summary>
  /// <returns></returns>
  private async Task OnSplashScreenAsync()
  {
    // Grab the ViewModel to update status
    var vm = ((MainWindow as Window)?.DataContext as SplashWindowViewModel);
    if (vm is null)
    {
      Debug.WriteLine("OnSplashScreen - Could not find DataContext");
      return;
    }

    // Perform user's custom loading
    await vm.CustomInitializationAsync();
  }
}
