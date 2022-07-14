using Prism.Regions;
using Xunit;

namespace Prism.Avalonia.Tests.Regions
{
    public class AllActiveRegionFixture
    {
        [Fact]
        public void AddingViewsToRegionMarksThemAsActive()
        {
            IRegion region = new AllActiveRegion();
            var view = new object();

            region.Add(view);

            Assert.True(region.ActiveViews.Contains(view));
        }

        [Fact]
        public void DeactivateThrows()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                IRegion region = new AllActiveRegion();
                var view = new object();
                region.Add(view);

                region.Deactivate(view);
            });

        }
    }
}
