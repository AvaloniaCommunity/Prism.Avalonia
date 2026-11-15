using Xunit;

// These fixtures replace process-wide Prism container and ViewModel locator state.
// Keep classes sequential so one fixture cannot reset another fixture's services.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
