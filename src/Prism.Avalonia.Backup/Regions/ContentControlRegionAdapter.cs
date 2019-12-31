using Prism.Properties;
using System;
using System.Collections.Specialized;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Prism.Avalonia.Properties;

namespace Prism.Regions
{
    /// <summary>
    /// Adapter that creates a new <see cref="SingleActiveRegion"/> and monitors its
    /// active view to set it on the adapted <see cref="ContentControl"/>.
    /// </summary>
    public class ContentControlRegionAdapter : RegionAdapterBase<ContentControl>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ContentControlRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        public ContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory, IRegionManager regionManager)
            : base(regionBehaviorFactory, regionManager)
        {
        }

        /// <summary>
        /// Adapts a <see cref="ContentControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, ContentControl regionTarget)
        {
            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            bool contentIsSet = regionTarget.Content != null;
            contentIsSet = contentIsSet /* || (BindingOperations.GetBinding(regionTarget, ContentControl.ContentProperty) != null) no analogs found for avalonia.
                Can this break something?*/;

            //if (contentIsSet)
            //    throw new InvalidOperationException(Resources.ContentControlHasContentException);

            region.ActiveViews.CollectionChanged += delegate
            {
                var firstActive = region.ActiveViews.FirstOrDefault();
                if (firstActive is IVisual uc)
                {
                    if (regionTarget is IVisual visual && visual.VisualChildren is IAvaloniaList<IVisual> list)
                    {
                        list.Remove(uc);
                    }
                }

                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };

            region.Views.CollectionChanged +=
                (sender, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add && region.ActiveViews.Count() == 0)
                    {
                        region.Activate(e.NewItems[0]);
                    }
                };
        }

        /// <summary>
        /// Creates a new instance of <see cref="SingleActiveRegion"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="SingleActiveRegion"/>.</returns>
        protected override IRegion CreateRegion(string name)
        {
            return new SingleActiveRegion()
            {
                Name = name,
                RegionManager = RegionManager
            };
        }
    }
}