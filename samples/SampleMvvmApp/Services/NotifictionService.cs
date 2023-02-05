using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace SampleMvvmApp.Services
{
    public class NotificationService : INotificationService
    {
        private int _notificationTimeout = 10;
        private WindowNotificationManager? _notificationManager;

        public int NotificationTimeout
        {
            get => _notificationTimeout;
            set
            {
                _notificationTimeout = (value < 0) ? 0 : value;
            }
        }

        /// <summary>Set the host window.</summary>
        /// <param name="hostWindow">Parent window.</param>
        public void SetHostWindow(TopLevel hostWindow)
        {
            var notificationManager = new WindowNotificationManager(hostWindow)
            {
                Position = NotificationPosition.BottomRight,
                MaxItems = 4,
                Margin = new Thickness(0, 0, 15, 40)
            };

            _notificationManager = notificationManager;
        }

        /// <summary>Display the notification.</summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="onClick">Optional OnClick action.</param>
        public void Show(string title, string message, Action? onClick = null)
        {
            if (_notificationManager is { } nm)
            {
                nm.Show(
                    new Notification(
                    title,
                    message,
                    NotificationType.Information,
                    TimeSpan.FromSeconds(_notificationTimeout),
                    onClick));
            }
        }
    }
}
