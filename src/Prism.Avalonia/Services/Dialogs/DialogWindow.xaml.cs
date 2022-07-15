using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Prism.Services.Dialogs
{
    /// <summary>Prism's default dialog host.</summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        /// <summary>
        /// The <see cref="IDialogResult"/> of the dialog.
        /// </summary>
        public IDialogResult Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogWindow"/> class.
        /// </summary>
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

        event CancelEventHandler IDialogWindow.Closing
        {
            add => throw new System.NotImplementedException();
            remove => throw new System.NotImplementedException();
        }

        ////public new Task ShowDialog(Window ownerWindow)
        ////{
        ////    throw new System.NotImplementedException();
        ////}
    }
}
