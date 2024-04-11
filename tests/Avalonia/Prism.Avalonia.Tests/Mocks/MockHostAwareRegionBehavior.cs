using Avalonia;
using Prism.Navigation.Regions.Behaviors;
using Prism.Regions;

namespace Prism.Avalonia.Tests.Mocks
{
    public class MockHostAwareRegionBehavior : IHostAwareRegionBehavior
    {
        public IRegion Region { get; set; }

        public void Attach()
        {

        }

        public AvaloniaObject HostControl { get; set; }
    }
}
