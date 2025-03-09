using System.Threading.Tasks;

namespace SampleSplashScreen.ViewModels;

/// <summary>Splash screen View Model.</summary>
public class SplashWindowViewModel : ViewModelBase
{
  private string _status = string.Empty;

  public SplashWindowViewModel()
  {
    // TODO:
    //  [X] Perform loading when called from App.axaml.cs and switch to MainWindow.
    //  [ ] Transition to new MainWindow after we're done here.
    Title = "Prism.Avalonia Splash Screen";
  }

  public string Greeting => "Prism Avalonia is starting";

  public string Status { get => _status; set => SetProperty(ref _status, value); }

  /// <summary>Called from App.axaml.cs to perform loading mechanism.</summary>
  /// <returns></returns>
  public async Task CustomInitializationAsync()
  {
    Status = "3";
    await Task.Delay(1000);

    Status = "2";
    await Task.Delay(1000);

    Status = "1";
    await Task.Delay(1000);
  }
}
