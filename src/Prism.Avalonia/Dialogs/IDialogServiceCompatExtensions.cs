using System;
using Avalonia.Controls;

namespace Prism.Dialogs
{
    /// <summary>Extensions for the IDialogService</summary>
    public static class IDialogServiceCompatExtensions
    {
        /// <summary>Shows a non-modal dialog.</summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void Show(this IDialogService dialogService, string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            parameters = EnsureShowNonModalParameter(parameters);
            dialogService.ShowDialog(name, parameters, new DialogCallback().OnClose(callback));
        }

        /// <summary>Shows a non-modal dialog.</summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        public static void Show(this IDialogService dialogService, string name, IDialogParameters parameters, Action<IDialogResult> callback, string windowName)
        {
            parameters = EnsureShowNonModalParameter(parameters);

            if (!string.IsNullOrEmpty(windowName))
                parameters.Add(KnownDialogParameters.WindowName, windowName);

            dialogService.ShowDialog(name, parameters, new DialogCallback().OnClose(callback));
        }

        /// <summary>Shows a non-modal dialog.</summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void Show(this IDialogService dialogService, string name)
        {
            var parameters = EnsureShowNonModalParameter(null);
            dialogService.Show(name, parameters, null);
        }

        /// <summary>Shows a non-modal dialog.</summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void Show(this IDialogService dialogService, string name, Action<IDialogResult> callback)
        {
            var parameters = EnsureShowNonModalParameter(null);
            dialogService.Show(name, parameters, callback);
        }

        ////public static void ShowDialog(this IDialogService dialogService, Window owner, string name, IDialogParameters parameters, DialogCallback callback)
        ////{
        ////
        ////    parameters ??= new DialogParameters();
        ////    var isModal = parameters.TryGetValue<bool>(KnownDialogParameters.ShowNonModal, out var show) ? !show : true;
        ////    var windowName = parameters.TryGetValue<string>(KnownDialogParameters.WindowName, out var wName) ? wName : null;
        ////    var ownerWin = parameters.TryGetValue<Window>(KnownDialogParameters.ParentWindow, out var hWnd) ? hWnd : null;
        ////
        ////    dialogService.ShowDialog(name, parameters, callback);
        ////}

        private static IDialogParameters EnsureShowNonModalParameter(IDialogParameters parameters)
        {
            parameters ??= new DialogParameters();

            if (!parameters.ContainsKey(KnownDialogParameters.ShowNonModal))
                parameters.Add(KnownDialogParameters.ShowNonModal, true);

            return parameters;
        }
    }
}
