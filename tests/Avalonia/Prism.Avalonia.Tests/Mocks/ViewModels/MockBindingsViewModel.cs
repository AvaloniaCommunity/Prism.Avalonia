using Prism.Mvvm;

namespace Prism.Avalonia.Tests.Mocks.ViewModels
{
    internal class MockBindingsViewModel : BindableBase
    {
        private int _mockProperty;

        public int MockProperty
        {
            get => _mockProperty;
            set => SetProperty(ref _mockProperty, value);
        }

        internal void InvokeOnPropertyChanged()
        {
            RaisePropertyChanged(nameof(MockProperty));
        }
    }
}
