using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace Prism.Dialogs
{
    /// <summary>Prism's default dialog host.</summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        /// <summary>The <see cref="IDialogResult"/> of the dialog.</summary>
        public IDialogResult Result { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DialogWindow"/> class.</summary>
        public DialogWindow()
        {
            InitializeComponent();

#if DEBUG
            //// this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ContentProperty)
            {
                //当WindowStyleProperty属性改变时，重新设置样式
                var obj = change.NewValue as AvaloniaObject;
                if (obj != null)
                {
                    //将obj的附加属性WindowStyle绑定到DialogWindow的附加属性WindowStyle上
                    var style = Dialog.GetWindowStyle(obj);
                    if (style != null)
                    {
                        this.Bind(Dialog.WindowStyleProperty, new Binding
                        {
                            Source = style,
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                    }

                }
            }
        }
    }
}
