# Upgrading from Prism 7.2 to 8.1

| Icon | Text | Description |
|------|------|-------------|
| ⚠️ | `:warning:` | Pending
| 🔳 | `:white_square_button:` | Underway
| ✔️ | `:heavy_check_mark:` | Done
| 💔 | `:broken_heart:` | Never implemented in this platform
| ❌ | `:x:` | Removed

[Icon Reference](https://github.com/markdown-templates/markdown-emojis)

## Action Items

* [ ] Upgrade Prism.Avalonia
* [ ] Upgrade Prism.DryIoc
* [ ] Upgrade Prism.Unity
* [ ] Remove IOCs not supported by Prism v8.1
* [ ] Restructure folders to match PrismLibrary
* [ ] Add Prism Dialogs for Avalonia
* [ ] Add Samples
* [ ] Add Unit Tests, matching PrismLibrary

## Upgrade Progress

### Prism.Avalonia

| File                                | Status |  Notes |
|-------------------------------------|--------|--------|
| PrismApplicationBase.cs             | :heavy_check_mark:
| Bootstrapper.cs                     | :heavy_check_mark: | Renamed to `PrismBootstrapperBase.cs`
| PrismBootstrapperBase.cs            | :heavy_check_mark: | Replaces `Boostrapper.cs`
| Common\MvvmHelpers.cs               | :white_square_button:
| Common\ObservableObject.cs          | :white_square_button:
| Common\UriParsingHelper.cs          | :white_square_button:
| Events\WeakDelegatesManager.cs      | :white_square_button:
| Extensions\CollectionExtensions.cs  | :white_square_button:
| Extensions\ExceptionExtension.cs    | :white_square_button:
| Extensions\ServiceLocationExtension.cs | :white_square_button:
| Ioc\IConainterRegistryExtension.cs  | :white_square_button:
| Logging\TextLogger.cs               | :x:
| Logging\TraceLogger.cs              | :x:
| Modularity\... |
| Mvvm\... |
| Properties\... |
| Regions\... |

(_more to come_)

### Prism.DryIoc

| File                          | Is New | Is Removed | Status |
|-------------------------------|--------|------------|--------|
| _Coming Soon_ |

### Prism.Unity

| File                          | Is New | Is Removed | Status |
|-------------------------------|--------|------------|--------|
| _Coming Soon_ |
