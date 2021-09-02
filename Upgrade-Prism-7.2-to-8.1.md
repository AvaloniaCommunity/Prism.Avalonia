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
| Extensions\CollectionExtensions.cs      | :white_square_button:
| Extensions\ExceptionExtension.cs        | :white_square_button:
| Extensions\ServiceLocationExtension.cs  | :white_square_button:
| Ioc\IConainterRegistryExtension.cs      | :white_square_button:
| Logging\TextLogger.cs                   | :x:
| Logging\TraceLogger.cs                  | :x:
| Modularity\AssemblyResolver.Desktop.cs                      | :white_square_button:
| Modularity\ConfigurationModuleCatalog.Desktop.cs            | :white_square_button:
| Modularity\ConfigurationStore.Desktop.cs                    | :white_square_button:
| Modularity\DirectoryModuleCatalog.Desktop.cs                | :white_square_button:
| Modularity\FileModuleTypeLoader.Desktop.cs                  | :white_square_button:
| Modularity\IAssemblyResolver.Desktop.cs                     | :white_square_button:
| Modularity\IConfigurationStore.Desktop.cs                   | :white_square_button:
| Modularity\IModuleCatalogExtensions.cs                      | :white_square_button:
| Modularity\IModuleGroupsCatalog.cs                          | :white_square_button:
| Modularity\IModuleTypeLoader.cs                             | :white_square_button:
| Modularity\ModuleAttribute.Desktop.cs                       | :white_square_button:
| Modularity\ModuleCatalog.cs                                 | :white_square_button:
| Modularity\ModuleConfigurationElement.Desktop.cs            | :white_square_button:
| Modularity\ModuleConfigurationElementCollection.Desktop.cs  | :white_square_button:
| Modularity\ModuleDependencyCollection.Desktop.cs            | :white_square_button:
| Modularity\ModuleDependencyConfigurationElement.Desktop.cs  | :white_square_button:
| Modularity\ModuleDownloadProgressChangedEventArgs.cs        | :white_square_button:
| Modularity\ModuleInfo.Desktop.cs                            | :white_square_button:
| Modularity\ModuleInfo.cs                                    | :white_square_button:
| Modularity\ModuleInfoGroup.cs                               | :white_square_button:
| Modularity\ModuleInfoGroupExtensions.cs                     | :white_square_button:
| Modularity\ModuleInitializer.cs                             | :white_square_button:
| Modularity\ModuleManager.Desktop.cs                         | :white_square_button:
| Modularity\ModuleManager.cs                                 | :white_square_button:
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

### Prism.DryIoc

| File                                | Status |  Notes |
|-------------------------------------|--------|--------|
| Ioc\DryIocContainerExtension.cs     | :white_square_button:
| Ioc\PrismIocExtensions.cs           | :white_square_button:
| Legacy\DryIocBootstrapper.cs        | :white_square_button:
| Legacy\DryIocExtensions.cs          | :white_square_button:
| Properties\Resources.Designer.resx  | :white_square_button:
| Properties\Resources.resx           | :white_square_button:
| DryIocServiceLocatorAdapter.cs      | :white_square_button:
| GlobalSuppressions.cs               | :white_square_button:
| PrismApplication.cs                 | :white_square_button:

### Prism.Unity

| File                                          | Status |  Notes |
|-----------------------------------------------|--------|--------|
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
