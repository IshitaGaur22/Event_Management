using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Data;
using Microsoft.AspNetCore.SignalR;
using Event_Management.Hubs;


namespace Event_Management.Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IBookingHistoryRepository _bookingRepo;
        private readonly IEmailService _emailService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext; // ✅ Added for SignalR

        public BookingHistoryService(IBookingHistoryRepository bookingRepo, IEmailService emailService, INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
        {
            _bookingRepo = bookingRepo;
            _emailService = emailService;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public Task<List<BookingHistoryDTO>> GetUpcomingBookings(int userId) =>
            _bookingRepo.GetUpcomingBookings(userId);

        public Task<List<BookingHistoryDTO>> GetPastBookings(int userId) =>
            _bookingRepo.GetPastBookings(userId);

        public Task<List<BookingHistoryDTO>> SearchByEventName(int userId, string eventName) =>
            _bookingRepo.SearchByEventName(userId, eventName);

        public Task<List<BookingHistoryDTO>> SearchByDate(int userId, DateOnly date) =>
            _bookingRepo.SearchByDate(userId, date);

        public async Task CancelBooking(int bookingId)
        {
            var booking = await _bookingRepo.GetBookingWithEvent(bookingId);
            if (booking == null) throw new Exception("Booking not found.");

            var today = DateOnly.FromDateTime(DateTime.Today);
            if (booking.Event.EventDate < today)
                throw new Exception("Cannot cancel past events.");

            if (booking.Status == "Cancelled")
                throw new Exception("Booking already cancelled.");

            // Check if cancellation is allowed (at least 1 hour before event start)
            var eventDateTime = booking.Event.EventDate.ToDateTime(booking.Event.EventTime);
            if ((eventDateTime - DateTime.Now).TotalMinutes <= 60)
                throw new Exception("Cannot cancel booking within 1 hour of the event start time.");

            // Only update status and seats if allowed
            booking.Status = "Cancelled";
            booking.Event.TotalSeats += booking.SelectedSeats;
            await _bookingRepo.SaveChangesAsync();

            //  Send Cancellation Email
            var subject = "Booking Cancelled";
            var body = $"Hi {booking.User.UserName},\n\nBooking ID: {booking.BookingId}\n\nYour booking for '{booking.Event.EventName}' on {booking.Event.EventDate} has been cancelled.\n\nThank you!";
            await _emailService.SendEmailAsync(booking.User.Email, subject, body);

            var notification = new Notification
            {
                UserId = booking.User.UserId,
                Message = subject + "\n" + body,
                Type = "BookingCancellation",
                CreatedAt = DateTime.Now
            };

            _notificationRepository.AddNotification(notification);
            _notificationRepository.SaveChangesAsync();


            await _hubContext.Clients.User(booking.User.UserId.ToString())
             .SendAsync("ReceiveNotification", new
             {
                 message = notification.Message,
                 createdAt = notification.CreatedAt
             });

        }
    }
}