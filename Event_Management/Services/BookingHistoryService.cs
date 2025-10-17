using Event_Management.DTOs;
using Event_Management.Repository;

namespace Event_Management.Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IBookingHistoryRepository _bookingRepo;

        //public BookingHistoryService(IBookingHistoryRepository bookingRepo)
        //{
        //    _bookingRepo = bookingRepo;
        //}
        private readonly IEmailService _emailService;

        public BookingHistoryService(IBookingHistoryRepository bookingRepo, IEmailService emailService)
        {
            _bookingRepo = bookingRepo;
            _emailService = emailService;
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

            booking.Status = "Cancelled";
            booking.Event.TotalSeats += booking.SelectedSeats;
            await _bookingRepo.SaveChangesAsync();

            // ✅ Send Cancellation Email
            var subject = "Booking Cancelled";
            var body = $"Hi {booking.User.UserName},\n\nYour booking for '{booking.Event.EventName}' on {booking.Event.EventDate} has been cancelled.\n\nThank you!";
            await _emailService.SendEmailAsync(booking.User.Email, subject, body);
        }
    }
}
