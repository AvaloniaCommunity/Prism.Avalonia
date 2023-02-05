# Change Log

Change log history for Prism.Avalonia

## v8.1.97.3-preview.11.5 (2022-02-??)

* New: Support for Avalonia v11.0 Preview 5
* New: Document [Upgrading-to-Avalonia-11.md]
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
* Upgraded sample projects to support latest Avalonia version

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
