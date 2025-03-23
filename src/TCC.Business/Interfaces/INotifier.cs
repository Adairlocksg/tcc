using TCC.Business.Notifications;

namespace TCC.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        void Handle(Notification notification);
        string GetNotificationMessage();
        List<Notification> GetNotifications();
    }
}
