using System;
using Avalonia;
using Prism.Common;
using Prism.Navigation.Regions;
using Prism.Properties;

namespace Prism.Navigation.Regions.Behaviors
{
    /// <summary>
    /// Behavior that synchronizes the <see cref="IRegion.Context"/> property of a <see cref="IRegion"/> with
    /// the control that hosts the Region. It does this by setting the <see cref="RegionManager.RegionContextProperty"/>
    /// Dependency Property on the host control.
    ///
    /// This behavior allows the usage of two way databinding of the RegionContext from XAML.
    /// </summary>
    public class SyncRegionContextWithHostBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        private const string RegionContextPropertyName = "Context";
        private AvaloniaObject hostControl;

        /// <summary>
        /// Name that identifies the SyncRegionContextWithHostBehavior behavior in a collection of RegionsBehaviors.
        /// </summary>
        public static readonly string BehaviorKey = "SyncRegionContextWithHost";

        private ObservableObject<object> HostControlRegionContext
        {
            get
            {
                return RegionContext.GetObservableContext(hostControl);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AvaloniaObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>
        /// A <see cref="AvaloniaObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="Control"/> that is part of the tree.
        /// </value>
        public AvaloniaObject HostControl
        {
            get
            {
                return hostControl;
            }
            set
            {
                if (IsAttached)
                {
                    throw new InvalidOperationException(Resources.HostControlCannotBeSetAfterAttach);
                }
                hostControl = value;
            }
        }

        /// <summary>
        /// Override this method to perform the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            if (HostControl != null)
            {
                // Sync values initially.
                SynchronizeRegionContext();

                // Now register for events to keep them in sync
                HostControlRegionContext.PropertyChanged += RegionContextObservableObject_PropertyChanged;
                Region.PropertyChanged += Region_PropertyChanged;
            }
        }

        private void Region_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RegionContextPropertyName)
            {
                if (RegionManager.GetRegionContext(HostControl) != Region.Context)
                {
                    // Setting this Dependency Property will automatically also change the HostControlRegionContext.Value
                    // (see RegionManager.OnRegionContextChanged())
                    RegionManager.SetRegionContext(hostControl, Region.Context);
                }
            }
        }

        private void RegionContextObservableObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                SynchronizeRegionContext();
            }
        }

        private void SynchronizeRegionContext()
        {
            // Forward this value to the Region
            if (Region.Context != HostControlRegionContext.Value)
            {
                Region.Context = HostControlRegionContext.Value;
            }

            // Also make sure the region's StyledProperty was changed (this can occur if the value
            // was changed only on the HostControlRegionContext)
            if (RegionManager.GetRegionContext(HostControl) != HostControlRegionContext.Value)
            {
                RegionManager.SetRegionContext(HostControl, HostControlRegionContext.Value);
            }
        }
    }
}
