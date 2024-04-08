# Upgrading Prism.Avalonia

This file documents the upgrade path of the Prism releases, noting files removed :x: , added :new:, and notes on breaking changes.

| Icon | Text | Description |
|------|------|-------------|
| 🔳 | `:white_square_button:` | Underway
| ✔️ | `:heavy_check_mark:` | Updated
| 🆕 | `:new:` | New
| ❌ | `:x:` | Removed
| ⚠️ | `:warning:` | Pending
| 💔 | `:broken_heart:` | Never implemented in this platform

* [Icon Reference](https://github.com/markdown-templates/markdown-emojis)

## Overview Prism 8.1.97 to 9.0.x

This file documents the upgrade path from Prism v8.1.97 to v9.0-pre support. Soon we will be moving this repo to be apart of the main Prism Library `:)`

Each of the following will be tagged and merged into the branch `Prism-9x` before being merged with `develop` and `master` branches.

### Progress

* [/] 9.0.271-pre - Will be tagged and released
* [ ] 9.0.401-pre - Will be tagged and released

### Release Comparison

* [DNF - 9.0.264-pre](https://github.com/PrismLibrary/Prism/compare/DNF...9.0.264-pre) - (_DNF = Dot Net Foundation_)
* [9.0.264-pre - 9.0.274-pre](https://github.com/PrismLibrary/Prism/compare/9.0.264-pre...9.0.271-pre)
* [9.0.274-pre - 9.0.401-pre](https://github.com/PrismLibrary/Prism/compare/9.0.271-pre...9.0.401-pre)

### Changes

* `Samples` folder renamed to `e2e`

#### Dialogs

**Breaking Changes:**

* Namespace changed from `Prism.Services.Dialogs` to `Prism.Dialogs`.
* `IDialogAware` property, `RequestClose`
  * Refactored from `event` to `property` (`event Action<IDialogResult> RequestClose;` -> `DialogCloseListener RequestClose { get; }`)
  * Property is now read-only

**Removed Files:**

| File                    | Status  |  Notes |
|-------------------------|---------|--------|
| `ButtonResult.cs`       | :x:     | Absorbed into `Prism.Core`
| `DialogParameters.cs`   | :x:     | Absorbed into `Prism.Core`
| `DialogResult.cs`       | :x:     | Absorbed into `Prism.Core`
| `IDialogParameters.cs`  | :x:     | Absorbed into `Prism.Core`
| `IDialogResult.cs`      | :x:     | Absorbed into `Prism.Core`

#### Generic

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|

## Upgrading from Prism 7.2 to 8.1

* Basis of comparison: [Prism Library v7.2.0.1422...v8.1.97](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.1.97)

### Action Items

* [X] Upgrade Prism.Avalonia
* [X] Upgrade Prism.DryIoc
* [X] Upgrade Prism.Unity
* [X] Remove IOCs not supported by Prism v8.1
* [X] Add Unit Tests, matching PrismLibrary
* [X] Upgrade Samples
* [X] Upgrade to .NET 6

### Out of Scope

* Add Prism Dialogs for Avalonia
  * This requires Avalonia v0.11 and [PR #8277](https://github.com/AvaloniaUI/Avalonia/pull/8277) as per [Issue 7908](https://github.com/AvaloniaUI/Avalonia/issues/7908).
* Restructure folders to match PrismLibrary

### Upgrade Progress

| File                  | Status  |  Notes |
|-----------------------|---------|--------|
| Readme.md             | :warning: | Needs updated to match 8.1.x NuGet package version
| src\Readme.md         | :new:     | Added from PrismLibrary v8.1.x

#### Prism.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Prism.Avalonia.csproj                     | :heavy_check_mark:            | Added `netcore` and `net45` targeting conditions, as per PrismLibrary v8.1.x
| PrismApplicationBase.cs                   | :heavy_check_mark:
| Bootstrapper.cs                           | :heavy_check_mark: :x:        | Renamed to `PrismBootstrapperBase.cs`
| PrismBootstrapperBase.cs                  | :heavy_check_mark: :new:      | Replaces `Boostrapper.cs`
| PrismInitializationExtensions.cs          | :heavy_check_mark: :warning: :new: | Needs, `SelectorRegionAdapter.cs`. Register container, Behavior, and Adapter goodies.
| Common\MvvmHelpers.cs                     | :heavy_check_mark:
| Common\ObservableObject.cs                | :heavy_check_mark: | Needs verified
| Common\UriParsingHelper.cs                | :heavy_check_mark:
| Events\WeakDelegatesManager.cs            | :heavy_check_mark: :x: | It's apart of `Prism.Events`
| Extensions\CollectionExtensions.cs        | :heavy_check_mark:
| Extensions\DependencyObjectExtensions.cs  | :warning: | Needs verified. Rename to `AvaloniaObjectionExtension`?
| Extensions\ExceptionExtension.cs          | :heavy_check_mark: :x:
| Extensions\ServiceLocationExtension.cs    | :heavy_check_mark: :x:
| Interactivity\CommandBehaviorBase.cs      | :heavy_check_mark: :warning: | Needs verification
| Interactivity\InvokeCommandAction.cs      | :warning: :new: |Needs converted to Avalonia; reference [AvaloniaBehaviors](https://github.com/wieslawsoltes/AvaloniaBehaviors/blob/master/src/Avalonia.Xaml.Interactions/Core/InvokeCommandAction.cs).
| Ioc\ContainerProviderExtension.cs         | :heavy_check_mark: :new: :warning: | Needs verification
| Ioc\IContainerRegistryExtensions.cs       | :heavy_check_mark:
| Logging\TextLogger.cs                     | :heavy_check_mark: :x: | Removed from Prism
| Logging\TraceLogger.cs                    | :heavy_check_mark: :x: | Removed from Prism
| Modularity\AssemblyResolver.Desktop.cs                        | :heavy_check_mark:
| Modularity\ConfigurationModuleCatalog.Desktop.cs              | :heavy_check_mark:
| Modularity\ConfigurationStore.Desktop.cs                      | :heavy_check_mark:
| Modularity\DirectoryModuleCatalog.Desktop.cs                  | :heavy_check_mark: :x:
| Modularity\DirectoryModuleCatalog.net45.cs                    | :heavy_check_mark: :new:
| Modularity\DirectoryModuleCatalog.netcore.cs                  | :heavy_check_mark: :new:
| Modularity\FileModuleTypeLoader.Desktop.cs                    | :heavy_check_mark:
| Modularity\IAssemblyResolver.Desktop.cs                       | :heavy_check_mark:
| Modularity\IConfigurationStore.Desktop.cs                     | :heavy_check_mark:
| Modularity\IModuleCatalogExtensions.cs                        | :heavy_check_mark:
| Modularity\IModuleGroupsCatalog.cs                            | :heavy_check_mark:
| Modularity\IModuleTypeLoader.cs                               | :heavy_check_mark:
| Modularity\ModuleAttribute.Desktop.cs                         | :heavy_check_mark:
| Modularity\ModuleCatalog.cs                                   | :heavy_check_mark:
| Modularity\ModuleConfigurationElement.Desktop.cs              | :heavy_check_mark:
| Modularity\ModuleConfigurationElementCollection.Desktop.cs    | :heavy_check_mark:
| Modularity\ModuleDependencyCollection.Desktop.cs              | :heavy_check_mark:
| Modularity\ModuleDependencyConfigurationElement.Desktop.cs    | :heavy_check_mark:
| Modularity\ModuleDownloadProgressChangedEventArgs.cs          | :heavy_check_mark: :x:
| Modularity\ModuleInfo.cs                                      | :heavy_check_mark:
| Modularity\ModuleInfo.Desktop.cs                              | :heavy_check_mark:
| Modularity\ModuleInfoGroup.cs                                 | :heavy_check_mark:
| Modularity\ModuleInfoGroupExtensions.cs                       | :heavy_check_mark:
| Modularity\ModuleInitializer.cs                               | :heavy_check_mark:
| Modularity\ModuleManager.cs                                   | :heavy_check_mark:
| Modularity\ModuleManager.Desktop.cs                           | :heavy_check_mark:
| Modularity\ModulesConfigurationSection.Desktop.cs             | :heavy_check_mark:
| Modularity\ModuleTypeLoaderNotFoundException.cs               | :heavy_check_mark:
| Modularity\ModuleTypeLoaderNotFoundException.Desktop.cs       | :heavy_check_mark:
| Modularity\XamlModuleCatalog.cs                               | :white_square_button: :new: | _disabled; only used by WPF, not XF, Maui or UNO._
| Mvvm\ViewModuleLocator.cs                                     | :heavy_check_mark:
| Properties\AssemblyInfo                                       | :heavy_check_mark:
| Properties\Resources.resx                                     | :heavy_check_mark:
| Properties\Resources.Designer.cs                              | :heavy_check_mark:
| Properties\Settings.Designer.cs                               | :heavy_check_mark:
| Properties\Settings.settings                                  | :heavy_check_mark:
| Regions\Behaviors\AutoPopulateRegionBehavior.cs               | :heavy_check_mark:
| Regions\Behaviors\BindRegionContextToAvaloniaObjectBehavior.cs | :heavy_check_mark:   | Equivilant, `BindRegionContextToDependencyObjectBehavior`
| Regions\Behaviors\ClearChildViewsRegionBehavior.cs            | :heavy_check_mark:
| Regions\Behaviors\DelayedRegionCreationBehavior.cs            | :heavy_check_mark: :warning:     | Needs Avalonia equivilant of `FrameworkContentElement += Loaded`
| Regions\Behaviors\DestructibleRegionBehavior.cs               | :heavy_check_mark: :new:
| Regions\Behaviors\IHostAwareRegionBehavior.cs                 | :heavy_check_mark:
| Regions\Behaviors\RegionActiveAwareBehavior.cs                | :heavy_check_mark:
| Regions\Behaviors\RegionCreationException.cs                  | :heavy_check_mark:
| Regions\Behaviors\RegionCreationException.Desktop.cs          | :heavy_check_mark:
| Regions\Behaviors\RegionManagerRegistrationBehavior.cs        | :heavy_check_mark:
| Regions\Behaviors\RegionMemberLifetimeBehavior.cs             | :heavy_check_mark:
| Regions\Behaviors\SelectorItemsSourceSyncBehavior.cs          | :white_square_button: :warning: | Needs attention
| Regions\Behaviors\SyncRegionContextWithHostBehavior.cs        | :heavy_check_mark:
| Regions\AllActiveRegion.cs                    | :heavy_check_mark:
| Regions\ContentControlRegionAdapter.cs        | :heavy_check_mark:
| Regions\DefaultRegionManagerAccessor.cs       | :heavy_check_mark:
| Regions\IConfirmNavigationRequest.cs          | :heavy_check_mark:
| Regions\IJournalAware.cs                      | :heavy_check_mark:
| Regions\INavigateAsync.cs                     | :heavy_check_mark:
| Regions\INavigationAware.cs                   | :heavy_check_mark:
| Regions\IRegion.cs                            | :heavy_check_mark:
| Regions\IRegionAdapter.cs                     | :heavy_check_mark:
| Regions\IRegionBehavior.cs                    | :heavy_check_mark:
| Regions\IRegionBehaviorCollection.cs          | :heavy_check_mark:
| Regions\IRegionBehaviorFactory.cs             | :heavy_check_mark:
| Regions\IRegionBehaviorFactoryExtensions.cs   | :heavy_check_mark: :new:
| Regions\IRegionCollection.cs                  | :heavy_check_mark:
| Regions\IRegionManager.cs                     | :heavy_check_mark:
| Regions\IRegionManagerAccessor.cs             | :heavy_check_mark:
| Regions\IRegionManagerExtensions.cs           | :heavy_check_mark: :new:
| Regions\IRegionMemberLifetime.cs              | :heavy_check_mark:
| Regions\IRegionNavigationContentLoader.cs     | :heavy_check_mark:
| Regions\IRegionNavigationJournal.cs           | :heavy_check_mark:
| Regions\IRegionNavigationJournalEntry.cs      | :heavy_check_mark:
| Regions\IRegionNavigationService.cs           | :heavy_check_mark:
| Regions\IRegionViewRegistry.cs                | :heavy_check_mark:
| Regions\IViewsCollection.cs                   | :heavy_check_mark:
| Regions\ItemMetadata.cs                       | :heavy_check_mark:
| Regions\ItemsControlRegionAdapter.cs          | :heavy_check_mark:
| Regions\NavigationAsyncExtensions.cs          | :heavy_check_mark:
| Regions\NavigationContext.cs                  | :heavy_check_mark:
| Regions\NavigationParameters.cs               | :heavy_check_mark:
| Regions\NavigationResult.cs                   | :heavy_check_mark:
| Regions\Region.cs                             | :heavy_check_mark:
| Regions\RegionAdapterBase.cs                  | :heavy_check_mark:
| Regions\RegionAdapterMappings.cs              | :heavy_check_mark:
| Regions\RegionBehavior.cs                     | :heavy_check_mark:
| Regions\RegionBehaviorCollection.cs           | :heavy_check_mark:
| Regions\RegionBehaviorFactory.cs              | :heavy_check_mark:
| Regions\RegionContext.cs                      | :heavy_check_mark:
| Regions\RegionManager.cs                      | :heavy_check_mark:
| Regions\RegionMemberLifetimeAttribute.cs      | :heavy_check_mark:
| Regions\RegionNavigationContentLoader.cs      | :heavy_check_mark:
| Regions\RegionNavigationEventArgs.cs          | :heavy_check_mark:
| Regions\RegionNavigationFailedEventArgs.cs    | :heavy_check_mark:
| Regions\RegionNavigationJournal.cs            | :heavy_check_mark:
| Regions\RegionNavigationJournalEntry.cs       | :heavy_check_mark:
| Regions\RegionNavigationService.cs            | :heavy_check_mark:
| Regions\RegionViewRegistry.cs                 | :heavy_check_mark:
| Regions\SelectorRegionAdapter.cs              | :white_square_button: :warning: | Needs attention - _Commented out and disabled in `PrismInitializationExtensions`_
| Regions\SingleActiveRegion.cs                 | :heavy_check_mark:
| Regions\SyncActiveStateAttribute.cs           | :heavy_check_mark:
| Regions\UpdateRegionsException.Desktop.cs     | :heavy_check_mark:
| Regions\UpdateRegionsException.cs             | :heavy_check_mark:
| Regions\ViewRegisteredEventArgs.cs            | :heavy_check_mark:
| Regions\ViewRegistrationException.Desktop.cs  | :heavy_check_mark:
| Regions\ViewRegistrationException.cs          | :heavy_check_mark:
| Regions\ViewSortHintAttribute.cs              | :heavy_check_mark:
| Regions\ViewsCollection.cs                    | :heavy_check_mark:
| Services\Dialogs\ButtonResult.cs              | :new: :heavy_check_mark:
| Services\Dialogs\Dialog.cs                    | :new: :heavy_check_mark:
| Services\Dialogs\DialogParameters.cs          | :new: :heavy_check_mark:
| Services\Dialogs\DialogResult.cs              | :new: :heavy_check_mark:
| Services\Dialogs\DialogService.cs             | :new: :heavy_check_mark:
| Services\Dialogs\DialogWindow.xaml            | :new: :heavy_check_mark:
| Services\Dialogs\DialogWindow.xaml.cs         | :new: :heavy_check_mark:
| Services\Dialogs\IDialogAware.cs              | :new: :heavy_check_mark:
| Services\Dialogs\IDialogParameters.cs         | :new: :heavy_check_mark:
| Services\Dialogs\IDialogResult.cs             | :new: :heavy_check_mark:
| Services\Dialogs\IDialogService.cs            | :new: :heavy_check_mark:
| Services\Dialogs\IDialogServiceExtensions.cs  | :new: :heavy_check_mark:
| Services\Dialogs\IDialogWindow.cs             | :new: :heavy_check_mark:
| Services\Dialogs\IDialogWindowExtensions.cs   | :new: :heavy_check_mark:

#### Containers

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

#### Prism.DryIoc.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| Prism.DryIoc.Avalonia.csproj.cs     | :heavy_check_mark:      | Updated to DryIoc v4.8.1
| DryIocServiceLocatorAdapter.cs      | :x:
| GlobalSuppressions.cs               | :heavy_check_mark:
| PrismApplication.cs                 | :heavy_check_mark:
| PrismBootstrapper.cs                | :new:
| Ioc\DryIocContainerExtension.cs     | :x:                     | Moved to `Containers`
| Ioc\PrismIocExtensions.cs           | :x:                     | Moved to `Containers`
| Legacy\DryIocBootstrapper.cs        | :x:
| Legacy\DryIocExtensions.cs          | :x:
| Properties\AssemblyInfo             | :new:
| Properties\Resources.Designer.resx  | :white_square_button:
| Properties\Resources.resx           | :white_square_button:

#### Prism.Unity.Avalonia

| File                                | Status  |  Notes |
|-------------------------------------|---------|--------|
| PrismApplication.cs                 | :heavy_check_mark:
| PrismBootstrapper.cs                | :heavy_check_mark:
| GlobalSuppressions.cs               | :heavy_check_mark:

#### Tests - Prism.Avalonia.Tests

Test writted to [Prism.WPF specs](https://github.com/PrismLibrary/Prism/tree/master/tests/Wpf/Prism.Wpf.Tests).

| File                                              | Status  |  Notes |
|---------------------------------------------------|---------|--------|
| Prism.Avalonia.Tests.csproj                       | :heavy_check_mark: | Upgraded to .NET 6 (_Prism v8.1.97 uses .NET v4.7.1_)
| CollectionChangedTracker.cs                       | :heavy_check_mark:
| CollectionExtensionsFixture.cs                    | :heavy_check_mark:
| CompilerHelper.Desktop.cs                         | :heavy_check_mark:
| ExceptionAssert.cs                                | :heavy_check_mark:
| ListDictionaryFixture.cs                          | :heavy_check_mark:
| CollectionChangedTracker.cs                       | :heavy_check_mark:
| PrismApplicationBaseFixture.cs                    | :heavy_check_mark: :warning:
| PrismBootstrapperBaseFixture.cs                   | :heavy_check_mark: | Throwing, _"protection level errors."_
| Logging\ ...                                      | :x:
| Interactivity\CommandBehaviorBaseFixture.cs       | :heavy_check_mark:
| Interactivity\InvokeCommandActionFixture.cs       | :heavy_check_mark:
| Mocks\Modules\MockAbstractModule.cs               | :heavy_check_mark:
| Mocks\Modules\MockAttributedModule.cs             | :heavy_check_mark:
| Mocks\Modules\MockDependantModule.cs              | :heavy_check_mark:
| Mocks\Modules\MockDependencyModule.cs             | :heavy_check_mark:
| Mocks\Modules\MockExposingTypeFromGacAssemblyModule.cs | :heavy_check_mark:
| Mocks\Modules\MockModuleA.cs                      | :heavy_check_mark:
| Mocks\Modules\MockModuleReferencedAssembly.cs     | :heavy_check_mark:
| Mocks\Modules\MockModuleReferencingAssembly.cs    | :heavy_check_mark:
| Mocks\Modules\MockModuleReferencingOtherModule.cs | :heavy_check_mark:
| Mocks\Modules\MockModuleThrowingException.cs      | :heavy_check_mark:
| Mocks\MockAsyncModuleTypeLoader.cs                | :heavy_check_mark:
| Mocks\MockClickableObject.cs                      | :heavy_check_mark:
| Mocks\MockCommand.cs                              | :heavy_check_mark:
| Mocks\MockConfigurationStore.Desktop.cs           | :heavy_check_mark:
| Mocks\MockContainerAdapter.cs                     | :heavy_check_mark:
| Mocks\MockDelegateReference.cs                    | :heavy_check_mark:
| Mocks\MockDependencyObject.cs                     | :heavy_check_mark:
| Mocks\MockFrameworkContentElement.cs              | :heavy_check_mark: :warning: |  [https://github.com/AvaloniaUI/Avalonia/pull/8277]
| Mocks\MockFrameworkElement.cs                     | :heavy_check_mark: :warning:
| Mocks\MockHostAwareRegionBehavior.cs              | :heavy_check_mark:
| Mocks\MockPresentationRegion.cs                   | :heavy_check_mark:
| Mocks\MockRegion.cs                               | :heavy_check_mark:
| Mocks\MockRegionAdapter.cs                        | :heavy_check_mark:
| Mocks\MockRegionBehavior.cs                       | :heavy_check_mark:
| Mocks\MockRegionBehaviorCollection.cs             | :heavy_check_mark:
| Mocks\MockRegionManager.cs                        | :heavy_check_mark:
| Mocks\MockRegionManagerAccessor.cs                | :heavy_check_mark:
| Mocks\MockSortableViews.cs                        | :heavy_check_mark:
| Mocks\MockViewsCollection.cs                      | :heavy_check_mark:
| ViewModels\MockOptOutViewModel.cs                 | :heavy_check_mark:
| ViewModels\MockViewModel.cs                       | :heavy_check_mark:
| Views\Mock.cs                                     | :heavy_check_mark:
| Views\MockOptOut.cs                               | :heavy_check_mark:
| Views\MockView.cs                                 | :heavy_check_mark:
| Modularity\AssemblyResolverFixture.Desktop.cs     | :heavy_check_mark:
| Modularity\ConfigurationModuleCatalogFixture.cs   | :heavy_check_mark:
| Modularity\ConfigurationStoreFixture.cs           | :heavy_check_mark:
| Modularity\DirectoryModuleCatalogFixture.cs       | :white_square_button: :warning:
| Modularity\FileModuleTypeLoaderFixture.Desktop.cs | :heavy_check_mark:
| Modularity\ModuleAttributeFixture.Desktop.cs      | :heavy_check_mark:
| Modularity\ModuleCatalogFixture.cs                | :heavy_check_mark:
| Modularity\ModuleDependencySolverFixture.cs       | :heavy_check_mark:
| Modularity\ModuleInfoGroupExtensionsFixture.cs    | :heavy_check_mark:
| Modularity\ModuleInfoGroupFixture.cs              | :heavy_check_mark:
| Modularity\ModuleInitializerFixture.cs            | :heavy_check_mark:
| Modularity\ModuleManagerExtensionsFixture.cs      | :heavy_check_mark:
| Modularity\ModuleManagerFixture.cs                | :heavy_check_mark:
| Modularity\XamlModuleCatalogFixture.txt.dll       | :heavy_check_mark:
| Modularity\DirectoryModuleCatalogFixture.cs       | :heavy_check_mark:
| Modularity\ModuleCatalogXml\InvalidDependencyModuleCatalog.xaml | :heavy_check_mark:
| Modularity\ModuleCatalogXml\SimpleModuleCatalog.xaml            | :heavy_check_mark:
| Mvvm\ViewModelLocatorFixture.cs                   | :heavy_check_mark:
| Regions\AllActiveRegionFixture.cs                 | :heavy_check_mark:
| Regions\ContentControlRegionAdapterFixture.cs     | :heavy_check_mark: :warning:
| Regions\ItemsControlRegionAdapterFixture.cs       | :white_square_button: :warning: [https://github.com/AvaloniaUI/Avalonia/issues/7553]
| Regions\LocatorNavigationTargetHandlerFixture.cs  | :heavy_check_mark:
| Regions\NavigationAsyncExtensionsFixture.cs       | :heavy_check_mark:
| Regions\NavigationContextFixture.cs               | :heavy_check_mark:
| Regions\NavigationParametersFixture.cs            | :heavy_check_mark:
| Regions\RegionAdapterBaseFixture.cs               | :heavy_check_mark:
| Regions\RegionAdapterMappingsFixture.cs           | :heavy_check_mark:
| Regions\RegionBehaviorCollectionFixture.cs        | :heavy_check_mark:
| Regions\RegionBehaviorFactoryFixture.cs           | :heavy_check_mark:
| Regions\RegionBehaviorFixture.cs                  | :heavy_check_mark:
| Regions\RegionFixture.cs                          | :heavy_check_mark:
| Regions\RegionManagerFixture.cs                   | :heavy_check_mark:
| Regions\RegionManagerRequestNavigateFixture.cs    | :heavy_check_mark:
| Regions\RegionNavigationJournalFixture.cs         | :heavy_check_mark:
| Regions\RegionNavigationServiceFixture.new.cs     | :heavy_check_mark:
| Regions\RegionViewRegistryFixture.cs              | :heavy_check_mark:
| Regions\SelectorRegionAdapterFixture.cs           | :white_square_button: :new: :warning:
| Regions\SingleActiveRegionFixture.cs              | :heavy_check_mark:
| Regions\ViewsCollectionFixture.cs                 | :heavy_check_mark:
| Regions\Behaviors\AutoPopulateRegionBehaviorFixture.cs                    | :heavy_check_mark:
| Regions\Behaviors\BindRegionContextToDependencyObjectBehaviorFixture.cs   | :heavy_check_mark:
| Regions\Behaviors\ClearChildViewsRegionBehaviorFixture.cs                 | :heavy_check_mark:
| Regions\Behaviors\DelayedRegionCreationBehaviorFixture.cs                 | :heavy_check_mark:
| Regions\Behaviors\RegionActiveAwareBehaviorFixture.cs                     | :heavy_check_mark:
| Regions\Behaviors\RegionManagerRegistrationBehaviorFixture.cs             | :heavy_check_mark:
| Regions\Behaviors\RegionMemberLifetimeBehaviorFixture.cs                  | :heavy_check_mark:
| Regions\Behaviors\SelectorItemsSourceSyncRegionBehaviorFixture.cs         | :white_square_button: :warning:
| Regions\Behaviors\SyncRegionContextWithHostBehaviorFixture.cs             | :heavy_check_mark:

#### Tests - Prism.Container.Avalonia.Shared

| File                                          | Status  |  Notes |
|-----------------------------------------------|---------|--------|
| Fixtures\ContainerExtensionCollection.cs      | :heavy_check_mark: |
| Mocks\NullModuleCatalogBootstrapper.cs        | :heavy_check_mark: |

#### Tests - Prism.DryIoc.Avalonia.Tests

**_TBD_**

| File                                          | Status  |  Notes |
|-----------------------------------------------|---------|--------|

## Prism Upgrade Comparison

As we all know, not everything is straight forward between these two XAML technologies. However, it's a good reminder to document the differences and 'got-yas'.

* [Prism v7.2.0.1422 to v8.0.0.1909](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.0.0.1909)
* [Prism v7.2.0.1422 to v8.1.97](https://github.com/PrismLibrary/Prism/compare/v7.2.0.1422...v8.1.97)
* [Prism v8.0.0.1909 to v8.1.97](https://github.com/PrismLibrary/Prism/compare/v8.0.0.1909...v8.1.97)

### Conversion Helpers

[https://docs.avaloniaui.net/misc/wpf/Control-frameworkelement-and-control]

| WPF                                       | Avalonia | Reference |
|-------------------------------------------|----------|-----------|
| `System.Windows`                          | `Avalonia`
| `System.Windows.FrameworkElement`         | `Avalonia.Controls.Control` | [Reference](https://docs.avaloniaui.net/misc/wpf/uielement-frameworkelement-and-control)
| `System.WIndows.FrameworkContentElement`  | `Avalonia.Controls.Control`
| `UIElement`                               | `Avalonia.Controls.Control`
| `ItemsControl.ItemsSource`                | `Avalonia.Controls.ItemsControl.ItemsSource` | [Naming Convention](https://github.com/AvaloniaUI/Avalonia/issues/7553#issuecomment-1032714913)
| `System.Windows.Markup.MarkupExtension`   | `Avalonia.Markup.Xaml.MarkupExtension` | [Reference](http://reference.avaloniaui.net/api/Avalonia.Markup.Xaml/MarkupExtension/)
| `System.Windows.Markup.ContentPropertyAttribute.ContentProperty` | `Avalonia.Metadata.Content` | [https://github.com/AvaloniaUI/Avalonia/pull/1126]
| `System.Windows.Markup`                   | `Avalonia.Markup.Xaml`
| `System.Windows.Markup.XmlnsDefinition`   | `Avalonia.Metadata.XmlnsDefinition`
| `System.Windows.DependencyObject`         | `Avalonia.AvaloniaObject`
| `System.Windows.DependencyProperty`       | `Avalonia.AvaloniaProperty`
| `System.Windows.DependencyPropertyChangedEventArgs` | `Avalonia.AvaloniaPropertyChangedEventArgs`
| `System.ComponentModel.DesignerProperties.GetIsInDesignMode(DependencyObject element);` | `Avalonia.Controls.Design.IsDesignMode;`
| `System.Windows.Controls.Primitives.Selector` | ?? | _used by `SelectorRegionAdapter.cs` and `PrismInitializationExtensions.cs`_
| `RoutedEventHandler`                      | _Not Implemented_

### AvaloniaProperty vs DependencyProperty

Note, Avalonia places the `propertyType` as part of `TValue` in its `..<THost, TValue>(...)`.

```cs
// Avalonia
public static AvaloniaProperty AutoWireViewModelProperty =
    AvaloniaProperty.RegisterAttached<Control, bool>(
        name: "AutoWireViewModel",
        ownerType: typeof(ViewModelLocator),
        defaultValue: false);

// WPF
public static DependencyProperty AutoWireViewModelProperty =
    DependencyProperty.RegisterAttached(
        name: "AutoWireViewModel",
        propertyType: typeof(bool?),
        ownerType: typeof(ViewModelLocator),
        defaultMetaData: new PropertyMetadata(defaultValue: null, propertyChangedCallback: AutoWireViewModelChanged));

```

### Behaviors and Triggers

In order to use Behaviors in Avalonia, you must download the [Avalonia XAML Behaviors](https://github.com/wieslawsoltes/AvaloniaBehaviors) [NuGet](https://www.nuget.org/packages/Avalonia.Xaml.Behaviors).

For example, [InvokeCommandActionView.axaml](https://github.com/wieslawsoltes/AvaloniaBehaviors/blob/master/samples/BehaviorsTestApplication/Views/Pages/InvokeCommandActionView.axaml) uses [InvokeCommandAction.cs](https://github.com/wieslawsoltes/AvaloniaBehaviors/blob/master/src/Avalonia.Xaml.Interactions/Core/InvokeCommandAction.cs).

### Inheriting WPF DependencyObject in Avalonia

```cs
// WPF:
public partial class ItemMetadata : DependencyObject
{
    ...
    private static void DependencyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        ItemMetadata itemMetadata = dependencyObject as ItemMetadata;
        if (itemMetadata != null)
        {
            itemMetadata.InvokeMetadataChanged();
        }
    }

// Avalonia:
public class ItemMetadata : AvaloniaObject
{
    ...
    private static void StyledPropertyChanged(IAvaloniaObject dependencyObject, AvaloniaPropertyChangedEventArgs args)
    {
        ItemMetadata itemMetadata = dependencyObject as ItemMetadata;
        if (itemMetadata != null)
        {
            itemMetadata.InvokeMetadataChanged();
        }
    }
```

### Property

Note
* Avalonia places WPF's `propertyType` as part of `TValue` in `<THost, TValue>`
* The `THost` object type is what is used in the Get and Set methods.

```cs
// Avalonia ------------
private static readonly AvaloniaProperty ObservableRegionContextProperty =
    AvaloniaProperty.RegisterAttached<Visual, ObservableObject<object>>(
        name: "ObservableRegionContext",
        ownerType: typeof(RegionContext));

static RegionContext()
{
    ObservableRegionContextProperty.Changed.Subscribe(args => GetObservableContext(args?.Sender as Visual));
}

// WPF -----------------
private static readonly DependencyProperty ObservableRegionContextProperty =
    DependencyProperty.RegisterAttached(
        name: "ObservableRegionContext",
        propertyType: typeof(ObservableObject<object>),
        ownerType: typeof(RegionContext),
        defaultMetadata: null);
```

### Property with Callback

Make sure to include, `using System;` or else `.Subscribe(..)` will throw an error.

```cs
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

// Avalonia ------------------
static ClassConstructor()
{
    RegionNameProperty.Changed.Subscribe(args => OnSetRegionNameCallback(args?.Sender, args));
}

public static readonly AvaloniaProperty RegionNameProperty = AvaloniaProperty.RegisterAttached<AvaloniaObject, string>(
    name: "RegionName",
    ownerType: typeof(RegionManager));

// WPF -----------------------
public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
    name: "RegionName",
    propertyType: typeof(string),
    ownerType: typeof(RegionManager),
    defaultMetadata: new PropertyMetadata(defaultValue: null, propertyChangedCallback: OnSetRegionNameCallback));
```
