using Prism.Commands;

namespace SampleSplashScreen.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private int _clickCounter = 0;

  public MainWindowViewModel()
  {
    Title = "Welcome to Prism.Avalonia, sponsored by Suess Labs!";
  }

  public string Greeting => "Welcome to Prism.Avalonia!";

  public int ClickCounter { get => _clickCounter; set => SetProperty(ref _clickCounter, value); }

  public DelegateCommand CmdIncrementCounter => new(() =>
  {
    ClickCounter++;
  });
}
