using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Prism.Regions;

namespace SampleMvvmApp.RegionAdapters
{
    /// <summary>
    /// Adapter that creates a new <see cref="AllActiveRegion"/> and binds all the views to the
    /// adapted <see cref="ItemsControl"/>.
    /// </summary>
    public class ItemsControlRegionAdapter : RegionAdapterBase<ItemsControl>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ItemsControlRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">
        /// The factory used to create the region behaviors to attach to the created regions.
        /// </param>
        public ItemsControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        /// Adapts an <see cref="ItemsControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, ItemsControl regionTarget)
        {
            // If the region or control are null, throw an exception
            if (region == null)
                throw new ArgumentNullException(nameof(region));
            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            // If control has child items, move them to the region in advance of binding the control
            // to the region.
            if (regionTarget.ItemCount > 0)
            {
                foreach (object? childItem in regionTarget.Items)
                {
                    region.Add(childItem);
                }
            }

            // Avalonia v11-Preview5 needs IRegion implement IList. Enforcing it to return
            // AvaloniaList<object> fixes this. Avalonia v11-Preview8 ItemsControl.Items is readonly
            // (#10827). regionTarget.Items = region.Views as
            // Avalonia.Collections.AvaloniaList<object>; var views = region.Views as
            // Avalonia.Collections.AvaloniaList<object>; regionTarget.ItemsSource = region.Views as Avalonia.Collections.AvaloniaList<object>;

            // Detect when an item has been added/removed to the ItemsControl's backing region. Copy
            // all items to a new collection and bind to the region's ItemsSource
            region.Views.CollectionChanged += (s, e) =>
            {
                var enumerator = region.Views.GetEnumerator();
                List<object> items = new();
                while (enumerator.MoveNext())
                {
                    items.Add(enumerator.Current);
                }
                regionTarget.ItemsSource = items;
            };
        }

        /// <summary>
        /// Creates a new instance of <see cref="AllActiveRegion"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="AllActiveRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }

        #endregion Protected Methods
    }
}
