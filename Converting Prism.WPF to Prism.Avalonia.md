# Converting Prism.WPF to Prism.Avalonia

As we all know, not everything is straight forward between these two XAML technologies. However, it's a good reminder to document the differences and 'got-yas'.

## Prism Upgrade Comparison

* [Prism v7.2.0.1422 to v8.0.0.1909](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.0.0.1909)
* [Prism v7.2.0.1422 to v8.1.97](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.1.97)
* [Prism v8.0.0.1909 to v8.1.97](https://github.com/PrismLibrary/Prism/compare/v8.0.0.1909...v8.1.97)

## Control Equivilants

[https://docs.avaloniaui.net/misc/wpf/Control-frameworkelement-and-control]

| WPF                     | Avalonia          | Notes |
|-------------------------|-------------------|-------|
| `UIElement`             | `Control`
| `DependencyProperty`    | `StyledProperty`
| `[ContentProperty(..)]` | `Content`         | [https://github.com/AvaloniaUI/Avalonia/pull/1126]
