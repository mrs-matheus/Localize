namespace Localize.Company.Domain.Notifications
{
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications = new();

        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool IsValid => !_notifications.Any();

        public void AddNotification(string key, string message)
            => _notifications.Add(new Notification(key, message));

        public void AddNotifications(IEnumerable<Notification> notifications)
            => _notifications.AddRange(notifications);
    }
}
