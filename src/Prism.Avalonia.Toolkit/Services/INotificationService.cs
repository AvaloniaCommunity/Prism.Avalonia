using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace Prism.Avalonia.Toolkit;

public interface INotificationService
{
    /// <summary>Defines the maximum number of notifications visible at once.</summary>
    int MaxItems { get; set; }

    /// <summary>The expiry time in seconds at which the notification will close (default 5 seconds).</summary>
    int NotificationTimeout { get; set; }

    /// <summary>Set the host window.</summary>
    /// <param name="hostWindow">Parent window.</param>
    void SetHostWindow(TopLevel window);

    /// <summary>Display the notification.</summary>
    /// <param name="title">Title.</param>
    /// <param name="message">Message.</param>
    /// <param name="type">The <see cref="NotificationType"/> of the notification.</param>
    /// <param name="onClick">An optional action to call when the notification is clicked.</param>
    /// <param name="onClose">An optional action to call when the notification is closed.</param>
    void Show(string title,
              string message,
              NotificationType type = NotificationType.Information,
              Action? onClick = null,
              Action? onClose = null);
}
