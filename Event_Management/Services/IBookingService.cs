using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IBookingService
    {
        //Post
        BookingSummary AddBooking(int selectedSeats, int userId, int eventId);

        //Get
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        public void UpdateBooking(Booking booking);
        User GetUserById(int userId);
        IEnumerable<Booking> GetBookingByName(string username);
        object GetSeats(int id);
        bool IsSeatAvailable(int eventId, int requestedSeats);
        IEnumerable<Booking> GetBookingsByEvent(int eventId);
        IEnumerable<Event> GetTopBookedEvents(int count);
        Payment GetPaymentByBookingId(int bookingId);

        //Put
        //int UpdateBooking(int id, Booking booking);
        void UpdateBooking(int id, UpdateBookingDto bookingDto);
        int UpdateCompletedBookings();

        //Delete
        int DeleteBooking(int id);


    }
}
