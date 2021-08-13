using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;

namespace Prism.Modularity
{
    /// <summary>
    /// The <see cref="ModuleCatalog"/> holds information about the modules that can be used by the
    /// application. Each module is described in a <see cref="ModuleInfo"/> class, that records the
    /// name, type and location of the module.
    ///
    /// It also verifies that the <see cref="ModuleCatalog"/> is internally valid. That means that
    /// it does not have:
    /// <list>
    ///     <item>Circular dependencies</item>
    ///     <item>Missing dependencies</item>
    ///     <item>
    ///         Invalid dependencies, such as a Module that's loaded at startup that depends on a module
    ///         that might need to be retrieved.
    ///     </item>
    /// </list>
    /// The <see cref="ModuleCatalog"/> also serves as a baseclass for more specialized Catalogs .
    /// </summary>
    ////[Portable.Xaml.Markup.ContentProperty("Items")]  // TODO: From package, Portable.Xaml.
    ////[ContentProperty("Items")]  // TODO: Using System.Windows.Markup
    public class ModuleCatalog : ModuleCatalogBase, IModuleGroupsCatalog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCatalog"/> class.
        /// </summary>
        public ModuleCatalog() : base()
        {
         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleCatalog"/> class while providing an
        /// initial list of <see cref="ModuleInfo"/>s.
        /// </summary>
        /// <param name="modules">The initial list of modules.</param>
        public ModuleCatalog(IEnumerable<ModuleInfo> modules) : base(modules)
        {
        }

        //
        // Summary:
        //     Gets the items in the Prism.Modularity.IModuleCatalog. This property is mainly
        //     used to add Prism.Modularity.IModuleInfoGroups or Prism.Modularity.IModuleInfos
        //     through XAML.
        [Content] 
        public new Collection<IModuleCatalogItem> Items => base.Items;

        /// <summary>
        /// Creates a <see cref="ModuleCatalog"/> from XAML.
        /// </summary>
        /// <param name="xamlStream"><see cref="Stream"/> that contains the XAML declaration of the catalog.</param>
        /// <returns>An instance of <see cref="ModuleCatalog"/> built from the XAML.</returns>
        public static ModuleCatalog CreateFromXaml(Stream xamlStream)
        {
            if (xamlStream == null)
            {
                throw new ArgumentNullException(nameof(xamlStream));
            }

            return AvaloniaRuntimeXamlLoader.Load(xamlStream, null) as ModuleCatalog;
        }

        /// <summary>
        /// Creates a <see cref="ModuleCatalog"/> from a XAML included as an Application Resource.
        /// </summary>
        /// <param name="builderResourceUri">Relative <see cref="Uri"/> that identifies the XAML included as an Application Resource.</param>
        /// <returns>An instance of <see cref="ModuleCatalog"/> build from the XAML.</returns>
        public static ModuleCatalog CreateFromXaml(Uri builderResourceUri)
        {
            // TODO: Not tested
            return AvaloniaXamlLoader.Load(builderResourceUri) as ModuleCatalog;
        }
    }
}
