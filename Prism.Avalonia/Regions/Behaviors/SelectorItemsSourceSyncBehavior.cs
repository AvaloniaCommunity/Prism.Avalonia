using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Prism.Avalonia.Properties;
using Prism.Properties;

namespace Prism.Regions.Behaviors
{
    /// <summary>
    /// Defines the attached behavior that keeps the items of the <see cref="SelectingItemsControl"/> host control in synchronization with the <see cref="IRegion"/>.
    /// 
    /// This behavior also makes sure that, if you activate a view in a region, the SelectedItem is set. If you set the SelectedItem or SelectedItems (ListBox)
    /// then this behavior will also call Activate on the selected items. 
    /// <remarks>
    /// When calling Activate on a view, you can only select a single active view at a time. By setting the SelectedItems property of a listbox, you can set
    /// multiple views to active. 
    /// </remarks>
    /// </summary>
    public class SelectorItemsSourceSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>
        /// Name that identifies the SelectorItemsSourceSyncBehavior behavior in a collection of RegionsBehaviors. 
        /// </summary>
        public static readonly string BehaviorKey = "SelectorItemsSourceSyncBehavior";
        private bool updatingActiveViewsInHostControlSelectionChanged;
        private SelectingItemsControl hostControl;

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>
        /// A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </value>
        /// <remarks>For this behavior, the host control must always be a <see cref="ItemsControl"/> or an inherited class.</remarks>
        public Visual HostControl
        {
            get
            {
                return this.hostControl;
            }

            set
            {
                this.hostControl = value as SelectingItemsControl;
            }
        }

        /// <summary>
        /// Starts to monitor the <see cref="IRegion"/> to keep it in synch with the items of the <see cref="HostControl"/>.
        /// </summary>
        protected override void OnAttach()
        {
            bool itemsSourceIsSet = this.hostControl.Items != null;
            itemsSourceIsSet = itemsSourceIsSet/* || (BindingOperations.GetBinding(this.hostControl, ItemsControl.ItemsSourceProperty) != null) no analogs found for avalonia.
                Can this break something? */;

            if (itemsSourceIsSet)
            {
                throw new InvalidOperationException(Resources.ItemsControlHasItemsSourceException);
            }

            this.SynchronizeItems();

            this.hostControl.SelectionChanged += this.HostControlSelectionChanged;
            this.Region.ActiveViews.CollectionChanged += this.ActiveViews_CollectionChanged;
            this.Region.Views.CollectionChanged += this.Views_CollectionChanged;
        }

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                int startIndex = e.NewStartingIndex;
                foreach (object newItem in e.NewItems)
                {
                    var items = this.hostControl.Items.Cast<object>().ToList();
                    items.Insert(startIndex++, newItem);

                    this.hostControl.Items = items;
                    this.hostControl.InvalidateVisual();
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object oldItem in e.OldItems)
                {
                    var items = this.hostControl.Items.Cast<object>().ToList();
                    items.Remove(oldItem);

                    this.hostControl.Items = items;
                    this.hostControl.InvalidateVisual();
                }
            }
        }

        private void SynchronizeItems()
        {
            List<object> existingItems = new List<object>();

            // Control must be empty before "Binding" to a region
            foreach (object childItem in this.hostControl.Items)
            {
                existingItems.Add(childItem);
            }

            foreach (object view in this.Region.Views)
            {
                var items = this.hostControl.Items.Cast<object>().ToList();
                items.Add(view);

                this.hostControl.Items = items;
                this.hostControl.InvalidateVisual();
            }

            foreach (object existingItem in existingItems)
            {
                this.Region.Add(existingItem);
            }
        }


        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.updatingActiveViewsInHostControlSelectionChanged)
            {
                // If we are updating the ActiveViews collection in the HostControlSelectionChanged, that 
                // means the user has set the SelectedItem or SelectedItems himself and we don't need to do that here now
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (this.hostControl.SelectedItem != null
                    && this.hostControl.SelectedItem != e.NewItems[0]
                    && this.Region.ActiveViews.Contains(this.hostControl.SelectedItem))
                {
                    this.Region.Deactivate(this.hostControl.SelectedItem);
                }

                this.hostControl.SelectedItem = e.NewItems[0];
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove &&
                     e.OldItems.Contains(this.hostControl.SelectedItem))
            {
                this.hostControl.SelectedItem = null;
            }
        }

        private void HostControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Record the fact that we are now updating active views in the HostControlSelectionChanged method. 
                // This is needed to prevent the ActiveViews_CollectionChanged() method from firing. 
                this.updatingActiveViewsInHostControlSelectionChanged = true;

                object source;
                source = e.Source;

                if (source == sender)
                {
                    foreach (object item in e.RemovedItems)
                    {
                        // check if the view is in both Views and ActiveViews collections (there may be out of sync)
                        if (this.Region.Views.Contains(item) && this.Region.ActiveViews.Contains(item))
                        {
                            this.Region.Deactivate(item);
                        }
                    }

                    foreach (object item in e.AddedItems)
                    {
                        if (this.Region.Views.Contains(item) && !this.Region.ActiveViews.Contains(item))
                        {
                            this.Region.Activate(item);
                        }
                    }
                }
            }
            finally
            {
                this.updatingActiveViewsInHostControlSelectionChanged = false;
            }
        }
    }
}
