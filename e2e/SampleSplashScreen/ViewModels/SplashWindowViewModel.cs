using System.Threading.Tasks;

namespace SampleSplashScreen.ViewModels;

public class SplashWindowViewModel : ViewModelBase
{
  private string _status = string.Empty;

  public SplashWindowViewModel()
  {
    Title = "Prism.Avalonia Splash Screen";
  }

  public string Greeting => "Prism Avalonia is starting";

  public string Status { get => _status; set => SetProperty(ref _status, value); }

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
