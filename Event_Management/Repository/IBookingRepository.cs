using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IBookingRepository
    {
        //Post
        IEnumerable<Booking> GetAllBookings();

        //Get
        Booking GetBookingById(int id);
        int AddBooking(Booking booking);
        Event GetLatestTicket();
        Event GetEventById(int eventId);
        User GetUserByUsername(string username);
        void UpdateBooking(Booking booking);
        IEnumerable<Booking> GetBookingByName(string username);
        object GetSeats(int id);
        IEnumerable<Booking> GetBookingsByEvent(int eventId);
        IEnumerable<Event> GetTopBookedEvents(int count);
        IEnumerable<Booking> GetPendingBookingsWithEvents();
        Payment GetPaymentByBookingId(int bookingId);
        

        //Put
        void UpdateTicket(Event ticket);
        void UpdateEventSeats(Event ev);
        int UpdateBooking(int id, Booking booking);
        int SaveUpdatedBookings(IEnumerable<Booking> bookings);

        //Delete
        int DeleteBooking(int id);

    }
}
