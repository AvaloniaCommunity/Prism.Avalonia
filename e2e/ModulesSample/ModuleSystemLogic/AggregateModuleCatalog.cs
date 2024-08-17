using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Modularity;

namespace ModulesSample.ModuleSystemLogic
{
    internal class AggregateModuleCatalog : IModuleCatalog
    {
        private readonly List<IModuleCatalog> _catalogs = new();

        /// <summary>Initializes a new instance of the <see cref="AggregateModuleCatalog"/> class.</summary>
        public AggregateModuleCatalog()
        {
            _catalogs.Add(new ModuleCatalog());
        }

        public ReadOnlyCollection<IModuleCatalog> Catalogs => _catalogs.AsReadOnly();

        /// <summary>Gets all the <see cref="ModuleInfo"/> classes that are in the case <see cref="ModuleCatalog"/></summary>
        public IEnumerable<IModuleInfo> Modules => Catalogs.SelectMany(x => x.Modules);

        IEnumerable<IModuleInfo> IModuleCatalog.Modules => Modules;

        /// <summary>Adds the catalog to the list of catalogs</summary>
        /// <param name="catalog">The catalog to add</param>
        public void AddCatalog(IModuleCatalog catalog)
        {
            if (catalog == null)
                throw new ArgumentException(nameof(catalog));

            _catalogs.Add(catalog);
        }

        /// <summary>Returns the list of <see cref="ModuleInfo"/> that <param name="moduleInfo" /> depends on.</summary>
        /// <param name="moduleInfo">The <see cref="ModuleInfo"/> to get.</param>
        /// <returns>An enumeration of <see cref="ModuleInfo"/> that <paramref name="moduleInfo"/> depends on.</returns>
        public IEnumerable<IModuleInfo> GetDependentModules(IModuleInfo moduleInfo)
        {
            var catalog = _catalogs.Single(x => x.Modules.Contains(moduleInfo));
            return catalog.GetDependentModules(moduleInfo);
        }

        /// <summary>
        ///   Returns the collection of <see cref="ModuleInfo"/> that contain both the <see cref="ModuleInfo"/>s in
        ///   <paramref name="modules"/>, but also all the modules they depend on.
        /// </summary>
        /// <param name="modules">The modules to get the dependencies for.</param>
        /// <returns>
        ///   A collection of <see cref="ModuleInfo"/> that contains both all <see cref="ModuleInfo"/>s in <paramref name="modules"/>
        ///   and also all the <see cref="ModuleInfo"/> they depend on
        /// </returns>
        public IEnumerable<IModuleInfo> CompleteListWithDependencies(IEnumerable<IModuleInfo> modules)
        {
            var modulesGroupedByCatalog = modules.GroupBy<IModuleInfo, IModuleCatalog>(module => _catalogs.Single(
               catalog => catalog.Modules.Contains(module)));

            return modulesGroupedByCatalog.SelectMany(x => x.Key.CompleteListWithDependencies(x));
        }

        /// <summary>Initializes the catalog, which may load and validate the modules.</summary>
        public void Initialize()
        {
            Catalogs.ToList().ForEach((catalog) => catalog.Initialize());
        }

        /// <summary>Adds a <see cref="ModuleInfo"/> to the <see cref="ModuleCatalog"/></summary>
        /// <param name="moduleInfo">The <see cref="ModuleInfo"/> to add</param>
        public IModuleCatalog AddModule(IModuleInfo moduleInfo)
        {
            return _catalogs[0].AddModule(moduleInfo);
        }
    }
}
