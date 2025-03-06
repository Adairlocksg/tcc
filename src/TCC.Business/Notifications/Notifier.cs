using TCC.Business.Interfaces;

namespace TCC.Business.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications = [];

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Count > 0;
        }
    }
}
