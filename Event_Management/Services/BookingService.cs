using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Data;

namespace Event_Management.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public BookingService(IBookingRepository repo, IPaymentRepository paymentRepository)
        {
            _bookingRepository = repo;
            _paymentRepository = paymentRepository;
        }

        
        //Post
        public BookingSummary AddBooking(int selectedSeats, string userName, int eventId)
        {
            var ev = _bookingRepository.GetEventById(eventId);
            if (ev == null || selectedSeats > ev.TotalSeats)
                throw new Exception("Invalid event or seat count.");

            var user = _bookingRepository.GetUserByUsername(userName);
            if (user == null)
                throw new Exception("User not found.");

            var booking = new Booking
            {
                EventId = ev.EventID,
                UserId = user.UserId,
                SelectedSeats = selectedSeats,
                BookingDate = DateTime.Now,
                Status = "Pending"
            };

            ev.TotalSeats -= selectedSeats;
            _bookingRepository.UpdateEventSeats(ev);
            _bookingRepository.AddBooking(booking);

            decimal amount = ev.PricePerTicket * selectedSeats;

            var payment = new Payment
            {
                EventID = ev.EventID,
                BookingId = booking.BookingId,
                Amount = amount,
                //TotalAmount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Credit Card",
                Status = "Completed"
            };

            _paymentRepository.AddPayment(payment);

            return new BookingSummary
            {
                EventName = ev.EventName,
                Location = ev.Location,
                EventDate = ev.EventDate,
                Time = ev.EventTime,
                PricePerTicket = ev.PricePerTicket,
                SelectedSeats = selectedSeats,
                TotalAmount = amount
            };
        }

        //Get
        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }
        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _bookingRepository.GetUserByUsername(username);
        }

        public IEnumerable<Booking> GetBookingByName(string username)
        {
            return _bookingRepository.GetBookingByName(username);
        }

        public object GetSeats(int id)
        {
            return _bookingRepository.GetSeats(id);
        }
    
        public bool IsSeatAvailable(int eventId, int requestedSeats)
        {
            var ev = _bookingRepository.GetEventById(eventId);
            return ev != null && ev.TotalSeats >= requestedSeats;
        }

        public IEnumerable<Booking> GetBookingsByEvent(int eventId)
        {
            return _bookingRepository.GetBookingsByEvent(eventId);
        }

        public IEnumerable<Event> GetTopBookedEvents(int count)
        {
            return _bookingRepository.GetTopBookedEvents(count);
        }
        public Payment GetPaymentByBookingId(int bookingId)
        {
            return _paymentRepository.GetPaymentByBookingId(bookingId);
        }


        //Put
        public void UpdateBooking(Booking booking)
        {
            _bookingRepository.UpdateBooking(booking);
        }
        public int UpdateCompletedBookings()
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            var bookingsToUpdate = _bookingRepository.GetPendingBookingsWithEvents()
                .Where(b => b.Event.EventDate < today ||
                           (b.Event.EventDate == today && b.Event.EndTime <= currentTime))
                .ToList();

            foreach (var booking in bookingsToUpdate)
            {
                booking.Status = "Completed";
            }

            return _bookingRepository.SaveUpdatedBookings(bookingsToUpdate);
        }

        public int UpdateBooking(int id, Booking booking)
        {
            return _bookingRepository.UpdateBooking(id, booking);
        }


        //Delete
        public int DeleteBooking(int id)
        {
            return _bookingRepository.DeleteBooking(id);
        }
    }
}
