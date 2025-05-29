using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Prism.Extensions;

namespace Prism.Dialogs
{
    /// <summary>
    /// This class contains <see cref="IDialogWindow"/> attached properties.
    /// </summary>
    public class Dialog
    {
        /// <summary>Identifies the WindowStyle attached property.</summary>
        /// <remarks>This attached property is used to specify the style of a <see cref="IDialogWindow"/>.</remarks>
        public static readonly AvaloniaProperty WindowStyleProperty =
            AvaloniaProperty.RegisterAttached<AvaloniaObject, Style>("WindowStyle", typeof(Dialog));

        /// <summary>Identifies the WindowStartupLocation attached property.</summary>
        /// <remarks>This attached property is used to specify the startup location of a <see cref="IDialogWindow"/>.</remarks>
        public static readonly AvaloniaProperty WindowStartupLocationProperty =
            AvaloniaProperty.RegisterAttached<AvaloniaObject, WindowStartupLocation>(
                name: "WindowStartupLocation",
                ownerType: typeof(Dialog));
        static Dialog()
        {
            WindowStyleProperty.Changed.AddClassHandler<AvaloniaObject>((o, e) => OnWindowStyleChanged(e?.Sender, (Style)e?.NewValue, (Style)e?.OldValue));
        }
        public Dialog()
        {
            WindowStartupLocationProperty.Changed.Subscribe(args => OnWindowStartupLocationChanged(args?.Sender, args));
        }

        /// <summary>
        /// Gets the value for the <see cref="WindowStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="WindowStyleProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static Style GetWindowStyle(AvaloniaObject obj)
        {
            return (Style)obj.GetValue(WindowStyleProperty);
        }

        /// <summary>
        /// Sets the <see cref="WindowStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The Style to attach.</param>
        public static void SetWindowStyle(AvaloniaObject obj, Style value)
        {
            obj.SetValue(WindowStyleProperty, value);
        }
        private static void OnWindowStyleChanged(AvaloniaObject sender, Style newStyle, Style oldStyle)
        {
            //获取sender顶级元素
            if (sender == null) return;
            //如果sender不是Window类型，则返回
            if (sender is not Window window) return; 
            //设置Window的样式 
            if (oldStyle != null) window.Styles.Remove(oldStyle);
            if (newStyle != null) window.Styles.Add(newStyle);
        }
        /// <summary>
        /// Gets the value for the <see cref="WindowStartupLocationProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="WindowStartupLocationProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static WindowStartupLocation GetWindowStartupLocation(AvaloniaObject obj)
        {
            return (WindowStartupLocation)obj.GetValue(WindowStartupLocationProperty);
        }

        /// <summary>
        /// Sets the <see cref="WindowStartupLocationProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The WindowStartupLocation to attach.</param>
        public static void SetWindowStartupLocation(AvaloniaObject obj, WindowStartupLocation value)
        {
            obj.SetValue(WindowStartupLocationProperty, value);
        }

        private static void OnWindowStartupLocationChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is Window window)
                window.WindowStartupLocation = (WindowStartupLocation)e.NewValue;
        }
    }
}
