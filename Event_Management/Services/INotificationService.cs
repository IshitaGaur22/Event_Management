using Event_Management.Models;

namespace Event_Management.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetUserNotifications(int userId);
        Task SendBookingConfirmationEmail(User user, Event evnt, Booking booking);
        Task SendCancellationEmail(User user, Event evnt, Booking booking);

    }
}
