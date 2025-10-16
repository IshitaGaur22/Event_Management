using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUserNotifications(int userId);
        Task AddNotification(Notification notification);
        Task SaveChangesAsync();
    }
}
