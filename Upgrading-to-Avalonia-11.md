# Upgrading to Avalonia v11 Preview

This document outlines the path to upgrading your projects from Avalonia v0.10.18 to v11.xx. Please read over this document and suggest changes to help improve the documentation. After all, we learn from one another.

Check out Avalonia's [Breaking Changes](https://github.com/AvaloniaUI/Avalonia/wiki/Breaking-Changes) wiki page for more information

## 11.0 Preview 5

* NEW: IDialogWindow now implements `WindowClosingEventArgs`.
  * See, [Issue #9524](https://github.com/AvaloniaUI/Avalonia/issues/9524), [PR #9715](https://github.com/AvaloniaUI/Avalonia/pull/9715)
  * This affects `IDialogWindow` implementation of `public event EventHandler<WindowClosingEventArgs>? Closing;`
* Deprecation of redundant interfaces.
  * See, [PR #9553](https://github.com/AvaloniaUI/Avalonia/pull/9553)
  * I.E. `IAvaloniaObject` -> `AvalonObject`, and more.
* Avalonia.ReactiveUI.Events.
  * See, [PR #5423](https://github.com/AvaloniaUI/Avalonia/pull/5423)
* Themes: Both Avalonia.Themes.Fluent and Avalonia.Themes.Simple (formally, Default) are not a part of the main Avalonia nuget package anymore. You need to add a PackageReference to include either of these packages or both. For more details, see #5593

### Known Issues

* [WindowNotificationManager Pop-Ups are no longer working in 11 Preview 5](https://github.com/AvaloniaUI/Avalonia/issues/10216)
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

