# Change Log

Change log history for Prism.Avalonia

## v9.0.401-pre (20204-04-13)

* Fixed typos
* DynamicallyAccessedMembers Attribute for linker hints
* Upgraded NuGet DryIoc to 5.4.3

## v9.0.271-pre (2024-04-12)

* Upgraded Prism.Core to v9.0.271-pre
* Breaking Changes:
  * `Prism.Dialog`
  * `Prism.Region` -> `Prism.Navigation.Region`

## v8.1.97.11072 (2024-01-27)

* Added support for .NET 8
* Upgraded Avalonia NuGet packages to 11.0.7
* Removed unused CommonServiceLocator

## v8.1.97.11000 (2023-07-05)

* Added support for Avalonia v11.0.0 GA Release

## v8.1.97.1021 (2023-06-01)

* NEW: Version numbering schema to denote Avalonia v0.10.21 (_1021_)
* Upgraded to support .NET 7, dropping .NET 5 support
* Upgraded to support Avalonia v0.10.21
* Updated sample projects and code cleanlyness
* Synced core components with latest develop

## v8.1.97.11-preview.11.8 (2023-05-31)

* Update: Converted ItemsControlRegionAdapter to use `ItemsSource` instead of `Items`
  * As of Avalonia PR [#10827](https://github.com/AvaloniaUI/Avalonia/pull/10827), `ItemsControl.Items` is readonly and should use `ItemsControls.ItemsSource` (PR [#10590](https://github.com/AvaloniaUI/Avalonia/pull/10590))

## v8.1.97.4-preview.11.5 (2022-02-??)

* New: Support for Avalonia v11.0 Preview 5
* New: Document [Upgrading-to-Avalonia-11.md](Upgrading-to-Avalonia-11.md)
* Update: IDialogWindow implements `WindowClosingEventArgs`. See, [Issue #9524](https://github.com/AvaloniaUI/Avalonia/issues/9524), [PR #9715](https://github.com/AvaloniaUI/Avalonia/pull/9715)
* Update: Avalonia interface objects deprecated - [PR #9553](https://github.com/AvaloniaUI/Avalonia/pull/9553)
* Removed: Reduced the package references
* Samples:
  * ALL: Renamed `.xaml` files to `.axaml` to comply with Avalonia's [XAML Name Reference Generator](https://github.com/AvaloniaUI/Avalonia.NameGenerator)
  * SampleMvvmApp - `WindowNotificationManager` implementation
  * SampleDialogApp - Added MessageBox-like dialog example with "title" and "message"
  * SampleDialogApp - Using Simple Theme instead of Fluent

## v8.1.97.3-preview.11.4 (2022-02-03)

* Added support for Avalonia v11.0 Preview 4
* Samples
  * Upgraded projects to support latest Avalonia version

## v8.1.97.2

* Upgrade Samples to .NET 6 by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/18
* Upgrade Prism.Avalonia.Unity to v8.1.97 by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/19
* .NET 6 Support by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/22
* IDialogService by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/21
* Dialog Service by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/23
* Sample Notification Pop-up by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/29
* Cleanup and Unit Test Repairs by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/30
* Update DryIoc to v4.8.0 to match Prism Library v8.1.97 by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/32
* Version Bump 8.1.97.2 by @DamianSuess in https://github.com/AvaloniaCommunity/Prism.Avalonia/pull/33

## v8.1.97.1 - 2022-12-08

* NEW: Automatically performs `AutoWireViewModel`
  * No longer need to device `prism:ViewModelLocator.AutoWireViewModel="True"` in View
* Updated DryIoc to v4.8.0
* Updated to Avalonia v0.10.18
* Fixed unit tests
* Spelling corrections
* Added Sample, Notificiation Pop-up Service

## v8.1.97 - 2022-07-14

* Upgraded to support Prism.Core v8.1.97
* See [[Upgrade-Prism-7.2-to-8.1.md]] for more details

## v7.2.0.1430

* Upgraded to support Prism.Core v7.2.0.1430
