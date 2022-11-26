using System;
using Avalonia;
using Prism.Regions;

namespace Prism.Avalonia.Tests.Mocks
{
    internal class MockRegionManagerAccessor : IRegionManagerAccessor
    {
        public Func<IAvaloniaObject, string> GetRegionName;
        public Func<IAvaloniaObject, IRegionManager> GetRegionManager;

        public event EventHandler UpdatingRegions;

        string IRegionManagerAccessor.GetRegionName(IAvaloniaObject element)
        {
            return this.GetRegionName(element);
        }

        IRegionManager IRegionManagerAccessor.GetRegionManager(IAvaloniaObject element)
        {
            if (this.GetRegionManager != null)
            {
                return this.GetRegionManager(element);
            }

            return null;
        }

        public void UpdateRegions()
        {
            if (this.UpdatingRegions != null)
            {
                this.UpdatingRegions(this, EventArgs.Empty);
            }
        }

        public int GetSubscribersCount()
        {
            return this.UpdatingRegions != null ? this.UpdatingRegions.GetInvocationList().Length : 0;
        }
    }
}
