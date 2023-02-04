using System.ComponentModel;
using System.Threading;
using System;
using Avalonia;
using Avalonia.Controls;

namespace Prism.Mvvm
{
    /// <summary>
    /// This class defines the attached property and related change handler that calls the ViewModelLocator in Prism.Mvvm.
    /// </summary>
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            // Bind AutoWireViewModelProperty.Changed to its callback
            AutoWireViewModelProperty.Changed.Subscribe(args => AutoWireViewModelChanged(args?.Sender, args));
        }

        /// <summary>
        /// The AutoWireViewModel attached property.
        /// </summary>
        public static AvaloniaProperty AutoWireViewModelProperty =
            AvaloniaProperty.RegisterAttached<Control, bool?>(
                name: "AutoWireViewModel",
                ownerType: typeof(ViewModelLocator),
                defaultValue: null);

        /// <summary>
        /// Gets the value for the <see cref="AutoWireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="AutoWireViewModelProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static bool? GetAutoWireViewModel(AvaloniaObject obj)
        {
            return (bool?)obj.GetValue(AutoWireViewModelProperty);
        }

        /// <summary>
        /// Sets the <see cref="AutoWireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The value to attach.</param>
        public static void SetAutoWireViewModel(AvaloniaObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            var value = (bool?)e.NewValue;
            if (value.HasValue && value.Value)
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
            if (view is Avalonia.Controls.Control element)
                element.DataContext = viewModel;
        }
    }
}
