# Upgrading from Prism 7.2 to 8.1

| Icon | Text | Description |
|------|------|-------------|
| 🔳 | `:white_square_button:` | Underway
| ✔️ | `:heavy_check_mark:` | Updated
| 🆕 | `:new:` | New
| ❌ | `:x:` | Removed
| ⚠️ | `:warning:` | Pending
| 💔 | `:broken_heart:` | Never implemented in this platform

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

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Readme.md                           | :warning: | Needs updated to match 8.1.x NuGet package version

### Prism.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Prism.Avalonia.csproj               | :heavy_check_mark: | Added `netcore` and `net45` targeting conditions, as per PrismLibrary v8.1.x
| PrismApplicationBase.cs             | :heavy_check_mark:
| Bootstrapper.cs                     | :x:     | Renamed to `PrismBootstrapperBase.cs`
| PrismBootstrapperBase.cs            | :new: :heavy_check_mark: | Replaces `Boostrapper.cs`
| PrismInitializationExtensions.cs    | :new: | All of the Register container, Behavior, and Adapter goodies.
| Common\MvvmHelpers.cs               | :white_square_button:
| Common\ObservableObject.cs          | :white_square_button:
| Common\UriParsingHelper.cs          | :white_square_button:
| Events\WeakDelegatesManager.cs      | :white_square_button:
| Extensions\CollectionExtensions.cs      | :white_square_button:
| Extensions\ExceptionExtension.cs        | :white_square_button:
| Extensions\ServiceLocationExtension.cs  | :white_square_button:
| Interactivity\CommandBehaviorBase.cs    | :new: :warning: | May require Avalonia switch
| Interactivity\InvokeCommandAction.cs    | :new:
| Ioc\ContainerProviderExtension.cs       | :new:
| Ioc\IContainerRegistryExtensions.cs     | :white_square_button:
| Logging\TextLogger.cs                   | :x: | Removed from Prism
| Logging\TraceLogger.cs                  | :x: | Removed from Prism
| Modularity\AssemblyResolver.Desktop.cs                      | :white_square_button:
| Modularity\ConfigurationModuleCatalog.Desktop.cs            | :white_square_button:
| Modularity\ConfigurationStore.Desktop.cs                    | :white_square_button:
| Modularity\DirectoryModuleCatalog.Desktop.cs                | :x: | Replaced by `.net45` and `.netcore` specific implimentations
| Modularity\DirectoryModuleCatalog.net45.cs                  | :new: |
| Modularity\DirectoryModuleCatalog.netcore.cs                | :new: |
| Modularity\FileModuleTypeLoader.Desktop.cs                  | :white_square_button:
| Modularity\IAssemblyResolver.Desktop.cs                     | :white_square_button:
| Modularity\IConfigurationStore.Desktop.cs                   | :white_square_button:
| Modularity\IModuleCatalogExtensions.cs                      | :white_square_button:
| Modularity\IModuleGroupsCatalog.cs                          | :heavy_check_mark:
| Modularity\IModuleTypeLoader.cs                             | :white_square_button:
| Modularity\ModuleAttribute.Desktop.cs                       | :white_square_button:
| Modularity\ModuleCatalog.cs                                 | :heavy_check_mark:
| Modularity\ModuleConfigurationElement.Desktop.cs            | :white_square_button:
| Modularity\ModuleConfigurationElementCollection.Desktop.cs  | :white_square_button:
| Modularity\ModuleDependencyCollection.Desktop.cs            | :white_square_button:
| Modularity\ModuleDependencyConfigurationElement.Desktop.cs  | :white_square_button:
| Modularity\ModuleDownloadProgressChangedEventArgs.cs        | :x: |
| Modularity\ModuleInfo.Desktop.cs                            | :white_square_button:
| Modularity\ModuleInfo.cs                                    | :white_square_button:
| Modularity\ModuleInfoGroup.cs                               | :white_square_button:
| Modularity\ModuleInfoGroupExtensions.cs                     | :white_square_button:
| Modularity\ModuleInitializer.cs                             | :white_square_button:
| Modularity\ModuleManager.cs                                 | :heavy_check_mark:
| Modularity\ModuleManager.Desktop.cs                         | :white_square_button:
| Modularity\ModulesConfigurationSection.Desktop.cs           | :white_square_button:
| Mvvm\ViewModuleLocator.cs         | :white_square_button:
| Properties\AssemblyInfo           | :white_square_button:
| Properties\Resources.Designer.cs  | :white_square_button:
| Properties\Resources.resx         | :white_square_button:
| Properties\Settings.Designer.cs   | :white_square_button:
| Properties\Settings.settings      | :white_square_button:
| Regions\Behaviors\AutoPopulateRegionBehavior.cs                 | :white_square_button:
| Regions\Behaviors\BindRegionContextToAvaloniaObjectBehavior.cs  | :white_square_button:
| Regions\Behaviors\ClearChildViewsRegionBehavior.cs              | :white_square_button:
| Regions\Behaviors\DelayedRegionCreationBehavior.cs              | :white_square_button:
| Regions\Behaviors\IHostAwareRegionBehavior.cs                   | :white_square_button:
| Regions\Behaviors\RegionActiveAwareBehavior.cs                  | :white_square_button:
| Regions\Behaviors\RegionCreationException.Desktop.cs            | :white_square_button:
| Regions\Behaviors\RegionCreationException.cs                    | :white_square_button:
| Regions\Behaviors\RegionManagerRegistrationBehavior.cs          | :white_square_button:
| Regions\Behaviors\RegionMemberLifetimeBehavior.cs               | :white_square_button:
| Regions\Behaviors\SelectorItemsSourceSyncBehavior.cs            | :white_square_button:
| Regions\Behaviors\SyncRegionContextWithHostBehavior.cs          | :white_square_button:
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
| Services\Dialogs\ButtonResult.cs              | :new: | New to Prism v8.1
| Services\Dialogs\Dialog.cs                    | :new: | New to Prism v8.1
| Services\Dialogs\DialogParameters.cs          | :new: | New to Prism v8.1
| Services\Dialogs\DialogResult.cs              | :new: | New to Prism v8.1
| Services\Dialogs\DialogService.cs             | :new: | New to Prism v8.1
| Services\Dialogs\DialogWindow.xaml            | :new: | New to Prism v8.1
| Services\Dialogs\DialogWindow.xaml.cs         | :new: | New to Prism v8.1
| Services\Dialogs\IDialogAware.cs              | :new: | New to Prism v8.1
| Services\Dialogs\IDialogParameters.cs         | :new: | New to Prism v8.1
| Services\Dialogs\IDialogResult.cs             | :new: | New to Prism v8.1
| Services\Dialogs\IDialogService.cs            | :new: | New to Prism v8.1
| Services\Dialogs\IDialogServiceExtensions.cs  | :new: | New to Prism v8.1
| Services\Dialogs\IDialogWindow.cs             | :new: | New to Prism v8.1
| Services\Dialogs\IDialogWindowExtensions.cs   | :new: | New to Prism v8.1


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

### Prism.DryIoc

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Ioc\DryIocContainerExtension.cs     | :white_square_button:
| Ioc\PrismIocExtensions.cs           | :heavy_check_mark:  | New
| Legacy\DryIocBootstrapper.cs        | :white_square_button:
| Legacy\DryIocExtensions.cs          | :white_square_button:
| Properties\Resources.Designer.resx  | :white_square_button:
| Properties\Resources.resx           | :white_square_button:
| DryIocServiceLocatorAdapter.cs      | :white_square_button:
| GlobalSuppressions.cs               | :white_square_button:
| PrismApplication.cs                 | :white_square_button:

### Prism.Unity

| File                                          | Status  |  Notes |
|-----------------------------------------------|---------|--------|
| Ioc\PrismIocExtensions.cs                     | :white_square_button:
| Ioc\UnityContainerExtension.cs                | :white_square_button:
| Legacy\UnityBootstrapper.cs                   | :white_square_button:
| Legacy\UnityContainerHelper.cs                | :white_square_button:
| Legacy\UnityExtensions.cs                     | :white_square_button:
| Properties\Resources.Designer.resx            | :white_square_button:
| Properties\Resources.resx                     | :white_square_button:
| Regions\UnityRegionNavigationContentLoader.cs | :white_square_button:
| PrismApplication.cs                           | :white_square_button:
| UnityServiceLocatorApplication.cs             | :white_square_button:
