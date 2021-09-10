using System.ComponentModel;
using Avalonia.Controls;

namespace Prism.Services.Dialogs
{
    /// <summary>
    /// Prism's default dialog host.
    /// </summary>
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
        }

        event CancelEventHandler IDialogWindow.Closing
        {
            add => throw new System.NotImplementedException();
            remove => throw new System.NotImplementedException();
        }

        public bool? ShowDialog()
        {
            throw new System.NotImplementedException();
        }
    }
}
