using System;
using Avalonia;
using Prism.Common;

namespace Prism.Regions
{
    /// <summary>
    /// Class that holds methods to Set and Get the RegionContext from a <seealso cref="AvaloniaObject"/>.
    ///
    /// RegionContext allows sharing of contextual information between the view that's hosting a <see cref="IRegion"/>
    /// and any views that are inside the Region.
    /// </summary>
    public static class RegionContext
    {
        private static readonly AvaloniaProperty ObservableRegionContextProperty =
            AvaloniaProperty.RegisterAttached<Visual, ObservableObject<object>>("ObservableRegionContext", typeof(RegionContext));

        static RegionContext()
        {
            ObservableRegionContextProperty.Changed.Subscribe(args => GetObservableContext(args?.Sender as Visual));
        }

        /// <summary>
        /// Returns an <see cref="ObservableObject{T}"/> wrapper around the RegionContext value. The RegionContext
        /// will be set on any views (dependency objects) that are inside the <see cref="IRegion.Views"/> collection by
        /// the <see cref="Behaviors.BindRegionContextToAvaloniaObjectBehavior"/> Behavior.
        /// The RegionContext will also be set to the control that hosts the Region, by the <see cref="Behaviors.SyncRegionContextWithHostBehavior"/> Behavior.
        ///
        /// If the <see cref="ObservableObject{T}"/> wrapper does not already exist, an empty one will be created. This way, an observer can
        /// notify when the value is set for the first time.
        /// </summary>
        /// <param name="view">Any view that hold the RegionContext value. </param>
        /// <returns>Wrapper around the <see cref="RegionContext"/> value. </returns>
        public static ObservableObject<object> GetObservableContext(AvaloniaObject view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            ObservableObject<object> context = view.GetValue(ObservableRegionContextProperty) as ObservableObject<object>;

            if (context == null)
            {
                context = new ObservableObject<object>();
                view.SetValue(ObservableRegionContextProperty, context);
            }

            return context;
        }
    }
}
