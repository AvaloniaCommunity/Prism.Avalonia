

using System.Windows;
using Prism.Regions;
using Prism.Regions.Behaviors;

namespace Prism.Avalonia.Tests.Mocks
{
    public class MockHostAwareRegionBehavior : IHostAwareRegionBehavior
    {
        public IRegion Region { get; set; }

        public void Attach()
        {
            
        }

        public DependencyObject HostControl { get; set; }
    }
}
