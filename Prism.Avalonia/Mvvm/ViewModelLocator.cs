using System.ComponentModel;
using System.Threading;
using System.Windows;
using System;
using Avalonia;

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
        public static Avalonia.AvaloniaProperty AutoWireViewModelProperty = 
            AvaloniaProperty.RegisterAttached<Avalonia.Controls.Control, bool>("AutoWireViewModel", typeof(ViewModelLocator), false);
        public static bool GetAutoWireViewModel(Avalonia.AvaloniaObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(Avalonia.AvaloniaObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(Avalonia.AvaloniaObject d,
            Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
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
            Avalonia.Controls.Control element = view as Avalonia.Controls.Control;
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
