using System;
using Avalonia;
using Prism.DryIoc;
using Prism.Ioc;
using SampleBaseApp.Views;

namespace SampleBaseApp;

public partial class App : PrismApplication
{
  /*
  public override void Initialize()
  {
      // Required when overriding Initialize()
      base.Initialize();
#if DEBUG
      // Replaces the old this.AttachDevTools();
      // NOTE: This requires connection to, http://127.0.0.1:29414/ and some IT firewalls may block it.
      // Reference: https://docs.avaloniaui.net/tools/developer-tools/attaching-to-the-remote-tool
      this.AttachDeveloperTools();
      ////{
      ////    // Change the initialization key gesture (Default is F12)
      ////    options.Gesture = Avalonia.Input.KeyGesture.Parse("F11");
      ////});
#endif
  }
  */

  protected override AvaloniaObject CreateShell()
  {
    System.Diagnostics.Debug.WriteLine("CreateShell()");
    return Container.Resolve<MainWindow>();
  }
}
