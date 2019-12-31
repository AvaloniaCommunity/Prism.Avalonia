using System.ComponentModel;
using System.Threading;
using System;
using Avalonia;
using Avalonia.Controls;

#if NETFX_CORE
using Windows.UI.Xaml;
#endif
namespace Prism.Mvvm
{
    /// <summary>
    /// This class defines the attached property and related change handler that calls the ViewModelLocator in Prism.Mvvm.
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// The AutoWireViewModel attached property.
        /// </summary>
        public static AvaloniaProperty AutoWireViewModelProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>(name: "AutoWireViewModel", ownerType: typeof(ViewModelLocator), defaultValue: false);
        public static bool GetAutoWireViewModel(AvaloniaObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(AvaloniaObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ViewModelLocationProvider.AutoWireViewModelChanged(d, Bind);
            }
        }

        /// <summary>
        /// Sets the DataContext of a View
        /// </summary>
        /// <param name="view">The View to set the DataContext on</param>
        /// <param name="viewModel">The object to use as the DataContext for the View</param>
        static void Bind(object view, object viewModel)
        {
            Control element = view as Control;
            if (element != null)
                element.DataContext = viewModel;
        }

        static ViewModelLocator()
        {
            // Bind AutoWireViewModelProperty.Changed to its callback
            AutoWireViewModelProperty.Changed.Subscribe(args => AutoWireViewModelChanged(args?.Sender, args));
        }
    }
}
