# Upgrading from Prism 7.2 to 8.1

| Icon | Text | Description |
|------|------|-------------|
| 🔳 | `:white_square_button:` | Underway
| ✔️ | `:heavy_check_mark:` | Updated
| 🆕 | `:new:` | New
| ❌ | `:x:` | Removed
| ⚠️ | `:warning:` | Pending
| 💔 | `:broken_heart:` | Never implemented in this platform

* [Icon Reference](https://github.com/markdown-templates/markdown-emojis)
* Basis of comparison: [Prism Library v7.2.0.1422...v8.1.97](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.1.97)

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

| File                  | Status  |  Notes |
|-----------------------|---------|--------|
| Readme.md             | :warning: | Needs updated to match 8.1.x NuGet package version
| src\Readme.md         | :new:     | Added from PrismLibrary v8.1.x

### Prism.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Prism.Avalonia.csproj               | :heavy_check_mark: | Added `netcore` and `net45` targeting conditions, as per PrismLibrary v8.1.x
| PrismApplicationBase.cs             | :warning: | In-progress; Needs upgraded to Avalonia
| Bootstrapper.cs                     | :heavy_check_mark: :x:     | Renamed to `PrismBootstrapperBase.cs`
| PrismBootstrapperBase.cs            | :new: :heavy_check_mark: | Replaces `Boostrapper.cs`
| PrismInitializationExtensions.cs    | :new: | All of the Register container, Behavior, and Adapter goodies.
| Common\MvvmHelpers.cs               | :heavy_check_mark:
| Common\ObservableObject.cs          | :white_square_button: :warning: | Needs Avalonia expert
| Common\UriParsingHelper.cs          | :heavy_check_mark:
| Events\WeakDelegatesManager.cs      | :heavy_check_mark: :x: | It's apart of `Prism.Events`
| Extensions\CollectionExtensions.cs        | :heavy_check_mark:
| Extensions\DependencyObjectExtensions.cs  | :warning: | Needs Avalonia expert
| Extensions\ExceptionExtension.cs          | :heavy_check_mark: :x:
| Extensions\ServiceLocationExtension.cs    | :heavy_check_mark: :x:
| Interactivity\CommandBehaviorBase.cs      | :heavy_check_mark: :warning: | Needs verification
| Interactivity\InvokeCommandAction.cs      | :new: :warning: | Has **ERRORS**; Needs converted to Avalonia
| Ioc\ContainerProviderExtension.cs         | :new: :heavy_check_mark: :warning: | Needs verification
| Ioc\IContainerRegistryExtensions.cs     | :heavy_check_mark:
| Logging\TextLogger.cs                   | :heavy_check_mark: :x: | Removed from Prism
| Logging\TraceLogger.cs                  | :heavy_check_mark: :x: | Removed from Prism
| Modularity\AssemblyResolver.Desktop.cs                      | :heavy_check_mark:
| Modularity\ConfigurationModuleCatalog.Desktop.cs            | :heavy_check_mark:
| Modularity\ConfigurationStore.Desktop.cs                    | :heavy_check_mark:
| Modularity\DirectoryModuleCatalog.Desktop.cs                | :heavy_check_mark: :x:
| Modularity\DirectoryModuleCatalog.net45.cs                  | :heavy_check_mark: :new: |
| Modularity\DirectoryModuleCatalog.netcore.cs                | :heavy_check_mark: :new: |
| Modularity\FileModuleTypeLoader.Desktop.cs                  | :heavy_check_mark:
| Modularity\IAssemblyResolver.Desktop.cs                     | :heavy_check_mark:
| Modularity\IConfigurationStore.Desktop.cs                   | :heavy_check_mark:
| Modularity\IModuleCatalogExtensions.cs                      | :heavy_check_mark:
| Modularity\IModuleGroupsCatalog.cs                          | :heavy_check_mark:
| Modularity\IModuleTypeLoader.cs                             | :heavy_check_mark:
| Modularity\ModuleAttribute.Desktop.cs                       | :heavy_check_mark:
| Modularity\ModuleCatalog.cs                                 | :heavy_check_mark:
| Modularity\ModuleConfigurationElement.Desktop.cs            | :heavy_check_mark:
| Modularity\ModuleConfigurationElementCollection.Desktop.cs  | :heavy_check_mark:
| Modularity\ModuleDependencyCollection.Desktop.cs            | :heavy_check_mark:
| Modularity\ModuleDependencyConfigurationElement.Desktop.cs  | :heavy_check_mark:
| Modularity\ModuleDownloadProgressChangedEventArgs.cs        | :x: |
| Modularity\ModuleInfo.cs                                    | :heavy_check_mark:
| Modularity\ModuleInfo.Desktop.cs                            | :heavy_check_mark:
| Modularity\ModuleInfoGroup.cs                               | :heavy_check_mark:
| Modularity\ModuleInfoGroupExtensions.cs                     | :heavy_check_mark:
| Modularity\ModuleInitializer.cs                             | :heavy_check_mark:
| Modularity\ModuleManager.cs                                 | :heavy_check_mark:
| Modularity\ModuleManager.Desktop.cs                         | :heavy_check_mark:
| Modularity\ModulesConfigurationSection.Desktop.cs           | :white_square_button:
| Modularity\XamlModuleCatalog.cs                             | :new: :warning: | Whats the Avalonia `XamlReader` equivilant?
| Mvvm\ViewModuleLocator.cs                                   | :heavy_check_mark:
| Properties\AssemblyInfo                                     | :heavy_check_mark:
| Properties\Resources.resx                                   | :heavy_check_mark:
| Properties\Resources.Designer.cs                            | :heavy_check_mark:
| Properties\Settings.Designer.cs                             | :heavy_check_mark:
| Properties\Settings.settings                                | :heavy_check_mark:
| Regions\Behaviors\AutoPopulateRegionBehavior.cs                 | :heavy_check_mark:
| Regions\Behaviors\BindRegionContextToAvaloniaObjectBehavior.cs  | :white_square_button: :warning: | Needs reviewed; Equivilant, `BindRegionContextToDependencyObjectBehavior`
| Regions\Behaviors\ClearChildViewsRegionBehavior.cs              | :heavy_check_mark:
| Regions\Behaviors\DelayedRegionCreationBehavior.cs              | :white_square_button: :warning: | Needs Avalonia equivilant of `FrameworkContentElement += Loaded`
| Regions\Behaviors\DestructibleRegionBehavior.cs                 | :heavy_check_mark: :new:
| Regions\Behaviors\IHostAwareRegionBehavior.cs                   | :heavy_check_mark:
| Regions\Behaviors\RegionActiveAwareBehavior.cs                  | :heavy_check_mark:
| Regions\Behaviors\RegionCreationException.cs                    | :heavy_check_mark:
| Regions\Behaviors\RegionCreationException.Desktop.cs            | :heavy_check_mark:
| Regions\Behaviors\RegionManagerRegistrationBehavior.cs          | :heavy_check_mark: :warning: | Needs reviewed
| Regions\Behaviors\RegionMemberLifetimeBehavior.cs               | :heavy_check_mark:
| Regions\Behaviors\SelectorItemsSourceSyncBehavior.cs            | :white_square_button: :warning: | Needs attention
| Regions\Behaviors\SyncRegionContextWithHostBehavior.cs          | :heavy_check_mark:
| Regions\AllActiveRegion.cs                    | :white_square_button:
| Regions\ContentControlRegionAdapter.cs        | :white_square_button:
| Regions\DefaultRegionManagerAccessor.cs       | :white_square_button:
| Regions\IConfirmNavigationRequest.cs          | :white_square_button:
| Regions\IJournalAware.cs                      | :white_square_button:
| Regions\INavigateAsync.cs                     | :white_square_button:
| Regions\INavigationAware.cs                   | :white_square_button:
| Regions\IRegion.cs                            | :white_square_button:
| Regions\IRegionAdapter.cs                     | :white_square_button:
| Regions\IRegionBehavior.cs                    | :white_square_button:
| Regions\IRegionBehaviorCollection.cs          | :white_square_button:
| Regions\IRegionBehaviorFactory.cs             | :white_square_button:
| Regions\IRegionCollection.cs                  | :white_square_button:
| Regions\IRegionManager.cs                     | :white_square_button:
| Regions\IRegionManagerAccessor.cs             | :white_square_button:
| Regions\IRegionMemberLifetime.cs              | :white_square_button:
| Regions\IRegionNavigationContentLoader.cs     | :white_square_button:
| Regions\IRegionNavigationJournal.cs           | :white_square_button:
| Regions\IRegionNavigationJournalEntry.cs      | :white_square_button:
| Regions\IRegionNavigationService.cs           | :white_square_button:
| Regions\IRegionViewRegistry.cs                | :white_square_button:
| Regions\IViewsCollection.cs                   | :white_square_button:
| Regions\ItemMetadata.cs                       | :white_square_button:
| Regions\ItemsControlRegionAdapter.cs          | :white_square_button:
| Regions\NavigationAsyncExtensions.cs          | :white_square_button:
| Regions\NavigationContext.cs                  | :white_square_button:
| Regions\NavigationParameters.cs               | :white_square_button:
| Regions\NavigationResult.cs                   | :white_square_button:
| Regions\Region.cs                             | :white_square_button:
| Regions\RegionAdapterBase.cs                  | :white_square_button:
| Regions\RegionAdapterMappings.cs              | :white_square_button:
| Regions\RegionBehavior.cs                     | :white_square_button:
| Regions\RegionBehaviorCollection.cs           | :white_square_button:
| Regions\RegionBehaviorFactory.cs              | :white_square_button:
| Regions\RegionContext.cs                      | :white_square_button:
| Regions\RegionManager.cs                      | :white_square_button:
| Regions\RegionMemberLifetimeAttribute.cs      | :white_square_button:
| Regions\RegionNavigationContentLoader.cs      | :white_square_button:
| Regions\RegionNavigationEventArgs.cs          | :white_square_button:
| Regions\RegionNavigationFailedEventArgs.cs    | :white_square_button:
| Regions\RegionNavigationJournal.cs            | :white_square_button:
| Regions\RegionNavigationJournalEntry.cs       | :white_square_button:
| Regions\RegionNavigationService.cs            | :white_square_button:
| Regions\RegionViewRegistry.cs                 | :white_square_button:
| Regions\SelectorRegionAdapter.cs              | :white_square_button:
| Regions\SingleActiveRegion.cs                 | :white_square_button:
| Regions\SyncActiveStateAttribute.cs           | :white_square_button:
| Regions\UpdateRegionsException.Desktop.cs     | :white_square_button:
| Regions\UpdateRegionsException.cs             | :white_square_button:
| Regions\ViewRegisteredEventArgs.cs            | :white_square_button:
| Regions\ViewRegistrationException.Desktop.cs  | :white_square_button:
| Regions\ViewRegistrationException.cs          | :white_square_button:
| Regions\ViewSortHintAttribute.cs              | :white_square_button:
| Regions\ViewsCollection.cs                    | :white_square_button:
| Services\Dialogs\ButtonResult.cs              | :new: :heavy_check_mark: |
| Services\Dialogs\Dialog.cs                    | :new: :warning: | Temp Disabled!
| Services\Dialogs\DialogParameters.cs          | :new: :heavy_check_mark: |
| Services\Dialogs\DialogResult.cs              | :new: :heavy_check_mark: |
| Services\Dialogs\DialogService.cs             | :new: :warning: | Temp Disabled!
| Services\Dialogs\DialogWindow.xaml            | :new: :warning: | Needs renamed to `axml`
| Services\Dialogs\DialogWindow.xaml.cs         | :new: :warning: | Has error, needs converted to Avalonia
| Services\Dialogs\IDialogAware.cs              | :new: :heavy_check_mark: |
| Services\Dialogs\IDialogParameters.cs         | :new: :heavy_check_mark: |
| Services\Dialogs\IDialogResult.cs             | :new: :heavy_check_mark: |
| Services\Dialogs\IDialogService.cs            | :new: :warning: | Needs multiplatform verification
| Services\Dialogs\IDialogServiceExtensions.cs  | :new: :warning: | Needs multiplatform verification
| Services\Dialogs\IDialogWindow.cs             | :new: :warning: | Temp Disabled!
| Services\Dialogs\IDialogWindowExtensions.cs   | :new: :warning: | Temp Disabled!

### Containers

Containers is a :new: Folder

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Containers\Prism.DryIoc.Shared\DryIocContainerExtension.cs    | :new:
| Containers\Prism.DryIoc.Shared\Prism.DryIoc.Shared.projitems  | :new:
| Containers\Prism.DryIoc.Shared\Prism.DryIoc.Shared.shproj     | :new:
| Containers\Prism.DryIoc.Shared\PrismIocExtensions.cs          | :new:
| Containers\Prism.Unity.Shared\Prism.Unity.Shared.projitems    | :new:
| Containers\Prism.Unity.Shared\Prism.Unity.Shared.shproj       | :new:
| Containers\Prism.Unity.Shared\PrismIocExntensions.cs          | :new:
| Containers\Prism.Unity.Shared\UnityContainerExtension.cs      | :new:

### Prism.DryIoc.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Prism.DryIoc.Avalonia.csproj.cs     | :heavy_check_mark: | Updated to DryIoc v4.8.1
| DryIocServiceLocatorAdapter.cs      | :x:
| GlobalSuppressions.cs               | :white_square_button:
| PrismApplication.cs                 | :heavy_check_mark:
| PrismBootstrapper.cs                | :new:
| Ioc\DryIocContainerExtension.cs     | :x:     | Moved to `Containers`
| Ioc\PrismIocExtensions.cs           | :x:     | Moved to `Containers`
| Legacy\DryIocBootstrapper.cs        | :white_square_button:
| Legacy\DryIocExtensions.cs          | :white_square_button:
| Properties\AssemblyInfo             | :new:
| Properties\Resources.Designer.resx  | :white_square_button:
| Properties\Resources.resx           | :white_square_button:

### Prism.Unity.Avalonia

| File                                          | Status  |  Notes |
|-----------------------------------------------|---------|--------|
| PrismApplication.cs                           | :white_square_button:
| UnityServiceLocatorApplication.cs             | :white_square_button:
| Ioc\PrismIocExtensions.cs                     | :x:     | Moved to `Containers`
| Ioc\UnityContainerExtension.cs                | :x:     | Moved to `Containers`
| Legacy\UnityBootstrapper.cs                   | :white_square_button:
| Legacy\UnityContainerHelper.cs                | :white_square_button:
| Legacy\UnityExtensions.cs                     | :white_square_button:
| Properties\Resources.Designer.resx            | :white_square_button:
| Properties\Resources.resx                     | :white_square_button:
| Regions\UnityRegionNavigationContentLoader.cs | :white_square_button:

## Conversion Helpers

| WPF                             | Avalonia | Reference |
|---------------------------------|----------|-----------|
| System.Windows.FrameworkElement | Avalonia.Controls.Control | [Reference](https://docs.avaloniaui.net/misc/wpf/uielement-frameworkelement-and-control) |
| System.WIndows.FrameworkContentElement | Avalonia.Controls.Control
| UIElement                       | Avalonia.Controls.Control |
| System.Windows.Markup.MarkupExtension | Avalonia.Markup.Xaml.MarkupExtension | [Reference](http://reference.avaloniaui.net/api/Avalonia.Markup.Xaml/MarkupExtension/)
| System.Windows.Markup.ContentPropertyAttribute.ContentProperty | Avalonia.Metadata.Content
| System.Windows.Markup | Avalonia.Markup.Xaml
| System.Windows.Markup.XmlnsDefinition | Avalonia.Metadata.XmlnsDefinition
| System.Windows.DependencyObject | Avalonia.AvaloniaObject

