using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Services;

namespace Event_Management.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IEmailService _emailService;

        public NotificationService(INotificationRepository notificationRepo, IEmailService emailService)
        {
            _notificationRepo = notificationRepo;
            _emailService = emailService;
        }


        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await _notificationRepo.GetUserNotifications(userId);
        }

        public async Task SendBookingConfirmationEmail(User user, Event evnt, Booking booking)
        {
            string subject = "Booking Confirmation";
            string body = $"Hi, your booking for {evnt.EventName} on {evnt.EventDate} at {evnt.EventTime} is confirmed.";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            await _notificationRepo.AddNotification(new Notification
            {
                UserId = user.UserId,
                Message = subject + ": " + body,
                Type = "Booking"
            });

            await _notificationRepo.SaveChangesAsync();
        }

        public async Task SendCancellationEmail(User user, Event evnt, Booking booking)
        {
            string subject = "Booking Cancelled";
            string body = $"Hi, your booking for {evnt.EventName} on {evnt.EventDate} has been cancelled.";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            await _notificationRepo.AddNotification(new Notification
            {
                UserId = user.UserId,
                Message = subject + ": " + body,
                Type = "Cancellation"
            });

            await _notificationRepo.SaveChangesAsync();
        }
    }
}
