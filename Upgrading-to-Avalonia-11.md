# Upgrading to Avalonia v11 Preview

This document outlines the path to upgrading your projects from Avalonia v0.10.18 to v11.xx. Please read over this document and suggest changes to help improve the documentation. After all, we learn from one another.

Check out Avalonia's [Breaking Changes](https://github.com/AvaloniaUI/Avalonia/wiki/Breaking-Changes) wiki page for more information

## 11.0 Preview 5

* NEW: Window now implements `public event EventHandler<WindowClosingEventArgs>? Closing;`
* Deprecation of redundant interfaces. See, [PR #9553](https://github.com/AvaloniaUI/Avalonia/pull/9553)
  * I.E. `IAvaloniaObject` -> `AvalonObject`, and more.
* Avalonia.ReactiveUI.Events. See, [PR #5423](https://github.com/AvaloniaUI/Avalonia/pull/5423)
* Themes: Both Avalonia.Themes.Fluent and Avalonia.Themes.Simple (formally, Default) are not a part of the main Avalonia nuget package anymore. You need to add a PackageReference to include either of these packages or both. For more details, see #5593

## 11.0 Preview 4

### DataTemplates

DataTemplates now require a `DataType` to be defined. This actually improves intellisense of your XAML and loading times with MVVM.

As a workaround for ListViews, you can use the `ItemTemplate` which does not require the definition of a `DataType`.

