using Avalonia;
using Avalonia.Controls;

namespace Prism.Regions.Behaviors
{
    /// <summary>
    /// Defines a <see cref="IRegionBehavior"/> that not allows extensible behaviors on regions which also interact
    /// with the target element that the <see cref="IRegion"/> is attached to.
    /// </summary>
    public interface IHostAwareRegionBehavior : IRegionBehavior
    {
        /// <summary>
        /// Gets or sets the <see cref="AvaloniaObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="AvaloniaObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="Control"/> that is part of the tree.</value>
        Visual HostControl { get; set; }
    }
}