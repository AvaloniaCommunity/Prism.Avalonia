# Upgrading to Avalonia v11 Preview

This document outlines the path to upgrading your projects from Avalonia v0.10.18 to v11.xx. Please read over this document and suggest changes to help improve the documentation. After all, we learn from one another.

Check out Avalonia's [Breaking Changes](https://github.com/AvaloniaUI/Avalonia/wiki/Breaking-Changes) wiki page for more information

* https://github.com/AvaloniaUI/Avalonia/compare/release/11.0.0-preview4...release/11.0.0-preview5
* https://github.com/AvaloniaUI/Avalonia/compare/11.0.0-preview3...11.0.0-preview4
* https://github.com/AvaloniaUI/Avalonia/compare/11.0.0-preview2...11.0.0-preview3
* https://github.com/AvaloniaUI/Avalonia/compare/11.0.0-preview1...11.0.0-preview2

## 11.0 Preview 5

**NOTE:** Breaking Changes Ahead!

### Breaking Changes

[Breaking Changes](https://github.com/AvaloniaUI/Avalonia/wiki/Breaking-Changes) wiki

* Interface Deprecation
  * See, [PR #9553](https://github.com/AvaloniaUI/Avalonia/pull/9553)
  * I.E. `IAvaloniaObject` -> `AvalonObject`, and more.
* [WindowNotificationManager Pop-Ups are no longer working in 11 Preview 5](https://github.com/AvaloniaUI/Avalonia/issues/10216)
  * See, [PR #9277](https://github.com/AvaloniaUI/Avalonia/pull/9277) and [example](https://github.com/AvaloniaUI/Avalonia/blob/master/samples/ControlCatalog/Pages/NotificationsPage.xaml.cs)
  * **Implementation example below**
  * The case was for "_single view platforms_" and not just Desktops which have a `Window` object.
* Themes:
  * Themes must be download as part of a separate package and `App.axaml` implementation has changed.
  * See, [PR# 8148](https://github.com/AvaloniaUI/Avalonia/pull/8166), [PR #8166](https://github.com/AvaloniaUI/Avalonia/pull/8166), [Issue #5593](https://github.com/AvaloniaUI/Avalonia/issues/5593)
  * NEW: `<SimpleTheme />`
  * OLD: `<SimpleTheme Mode="Light" />`
  * Both Avalonia.Themes.Fluent and Avalonia.Themes.Simple (formally, Default) are not a part of the main Avalonia nuget package anymore. You need to add a PackageReference to include either of these packages or both.
  * Its been observed that when setting a background image in your control or window, all of the controls will appear cloudy.  _Upgrade or defect?_

### Updates

* NEW: IDialogWindow now implements `WindowClosingEventArgs`.
  * See, [Issue #9524](https://github.com/AvaloniaUI/Avalonia/issues/9524), [PR #9715](https://github.com/AvaloniaUI/Avalonia/pull/9715)
  * This affects `IDialogWindow` implementation of `public event EventHandler<WindowClosingEventArgs>? Closing;`
* Avalonia.ReactiveUI.Events.
  * See, [PR #5423](https://github.com/AvaloniaUI/Avalonia/pull/5423)

### WindowNotificationManager Example

Previously, users had to set the HostWindow inside the main shell Window as you see below. Now, users can define this from any UserControl view by simply providing `notifyService.SetHostWindow(TopLevel.GetTopLevel(this))` in the view's .axaml.cs `override void OnAttachedToVisualTree(..)` method.

```cs
public partial class DashboardView : UserControl
{
  public DashboardView()
  {
    InitializeComponent();
  }

  protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
  {
    base.OnAttachedToVisualTree(e);

    // Initialize the WindowNotificationManager with the MainWindow
    var notifyService = ContainerLocator.Current.Resolve<INotificationService>();
    notifyService.SetHostWindow(TopLevel.GetTopLevel(this));
  }
}

```

### Known Issues


* Themes in sample are showing up cloudy
* Selected ListView item still appears after clearing the List
  * **STATUS:** _Needs reported_
  * **Reproduce:**
    * Add items to ListView to fill 2+ pages
    * Scroll down to select item
    * Clear list
    * Resize window to show area previous now shown
  * **Result:**
    * Selected item still appears in list despite items removed from collection

### Themes

When using Fluent theme, you no longer has a `Mode` attribute.

```xml
  <!-- New -->
  <FluentTheme />

  <!-- Old -->
  <FluentTheme Mode="Light" />
```


## 11.0 Preview 4

### DataTemplates

DataTemplates now require a `DataType` to be defined. This actually improves intellisense of your XAML and loading times with MVVM.

As a workaround for ListViews, you can use the `ItemTemplate` which does not require the definition of a `DataType`.

