using System;
using Avalonia.Controls;

namespace Prism.Services.Dialogs
{
    /// <summary>Interface to show modal and non-modal dialogs.</summary>
    public interface IDialogService
    {
        /// <summary>Shows a non-modal dialog.</summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback = null, string windowName = null);

        /// <summary>Shows a modal dialog.</summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback = null, string windowName = null);

        /// <summary>Shows a modal dialog attached to the defined parent.</summary>
        /// <param name="owner">The optional host window of the modal dialog.</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        void ShowDialog(Window owner, string name, IDialogParameters parameters, Action<IDialogResult> callback = null, string windowName = null);
    }
}
