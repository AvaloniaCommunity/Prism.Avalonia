using System;
using Avalonia;
using Prism.DryIoc;
using Prism.Ioc;
using SampleBaseApp.Views;

namespace SampleBaseApp;

public partial class App : PrismApplication
{
  protected override AvaloniaObject CreateShell()
  {
    System.Diagnostics.Debug.WriteLine("CreateShell()");
    return Container.Resolve<MainWindow>();
  }
}
