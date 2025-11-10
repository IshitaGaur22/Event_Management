using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Services;
using Microsoft.AspNetCore.SignalR;
using Event_Management.Hubs;

namespace Event_Management.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IEmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(INotificationRepository notificationRepo, IEmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _notificationRepo = notificationRepo;
            _emailService = emailService;
            _hubContext = hubContext;
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await _notificationRepo.GetUserNotifications(userId);
        }

        private async Task SendRealtimeNotification(Notification notification)
        {
            var userIdStr = notification.UserId.ToString();

            // send structured payload (not only string) — frontend can push to notifications tab
            await _hubContext.Clients.User(userIdStr).SendAsync("ReceiveNotification", notification);
            // fallback to the per-user group
            await _hubContext.Clients.Group($"user_{userIdStr}").SendAsync("ReceiveNotification", notification);
        }

        public async Task SendBookingConfirmationEmail(User user, Event evnt, Booking booking)
        {
            string subject = "Booking Confirmation";
            string body = $"Hi {user.UserName}, your booking for {evnt.EventName} on {evnt.EventDate} at {evnt.EventTime} is confirmed.";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            var notification = new Notification
            {
                UserId = user.UserId,
                Message = subject + ": " + body,
                Type = "Booking"
            };

            await _notificationRepo.AddNotification(notification);
            await _notificationRepo.SaveChangesAsync();

            await SendRealtimeNotification(notification);
        }

        public async Task SendCancellationEmail(User user, Event evnt, Booking booking)
        {
            string subject = "Booking Cancelled";
            string body = $"Hi {user.UserName}, your booking for {evnt.EventName} on {evnt.EventDate} has been cancelled.";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            var notification = new Notification
            {
                UserId = user.UserId,
                Message = subject + ": " + body,
                Type = "Cancellation"
            };

            await _notificationRepo.AddNotification(notification);
            await _notificationRepo.SaveChangesAsync();

            await SendRealtimeNotification(notification);
        }

        public async Task SendEventReminder(User user, Event evnt)
        {
            string subject = "Event Reminder";
            string body = $"Reminder: '{evnt.EventName}' starts at {evnt.EventTime} today.";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            var notification = new Notification
            {
                UserId = user.UserId,
                Message = subject + ": " + body,
                Type = "EventReminder"
            };

            await _notificationRepo.AddNotification(notification);
            await _notificationRepo.SaveChangesAsync();

            await SendRealtimeNotification(notification);
        }
    }
}