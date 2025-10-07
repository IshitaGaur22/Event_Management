using Event_Management.Models;
using Event_Management.Repository;
using EventManagement.Data;

namespace Event_Management.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository repo)
        {
            _bookingRepository = repo;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public int AddBooking(int selectedSeats)
        {
            var ticket = _bookingRepository.GetLatestTicket();
            if (ticket == null || selectedSeats < ticket.TotalSeats)
                throw new Exception("Invalid ticket or seat count.");

            var booking = new Booking
            {
                TicketId = ticket.TicketId,
                EventId = ticket.EventId,
                //UserId = 1, 
                SelectedSeats = selectedSeats,
                BookingDate = DateTime.Now
            };

            ticket.TotalSeats -= selectedSeats;
            _bookingRepository.UpdateTicket(ticket);

            return _bookingRepository.AddBooking(booking);
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }
    }
}
