using Event_Management.DTOs;
using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;

namespace Event_Management.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;

        public BookingService(IBookingRepository repo, IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _bookingRepository = repo;
            _paymentRepository = paymentRepository;
            _emailService = emailService;
        }

        // Post
        public BookingSummary AddBooking(int selectedSeats, string userName, int eventId)
        {
            var ev = _bookingRepository.GetEventById(eventId);

            if (ev == null)
                throw new EventsNotFoundException(eventId);

            if (selectedSeats <= 0 || selectedSeats > ev.TotalSeats)
                throw new SeatsUnavailableException(eventId, selectedSeats, ev.TotalSeats);

            var user = _bookingRepository.GetUserByUsername(userName);

            if (user == null)
                throw new UserNotFoundException(userName);

            // Create Booking
            var booking = new Booking
            {
                EventId = ev.EventID,
                UserId = user.UserId,
                SelectedSeats = selectedSeats,
                BookingDate = DateTime.Now,
                Status = "Upcoming"
            };

            ev.TotalSeats -= selectedSeats;
            _bookingRepository.UpdateEventSeats(ev);
            _bookingRepository.AddBooking(booking);

            // Create Payment
            decimal amount = ev.PricePerTicket * selectedSeats;
            var payment = new Payment
            {
                EventID = ev.EventID,
                BookingId = booking.BookingId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Credit Card",
                Status = "Completed"
            };
            _paymentRepository.AddPayment(payment);

            // Send Confirmation Email
            var subject = "Booking Confirmation";
            var body = $"Hi {user.UserName},\n\nYour booking for '{ev.EventName}' on {ev.EventDate} at {ev.EventTime} is confirmed.\n\nTotal Amount: {amount:C}\n\nThank you!";
            _emailService.SendEmailAsync(user.Email, subject, body);

            return new BookingSummary
            {
                EventName = ev.EventName,
                Location = ev.Location,
                Time = ev.EventTime,
                PricePerTicket = ev.PricePerTicket,
                SelectedSeats = selectedSeats,
                TotalAmount = amount
            };
        }

        // Get
        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public Booking GetBookingById(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
                throw new BookingNotFoundException(id);
            return booking;
        }

        public User GetUserByUsername(string username)
        {
            var user = _bookingRepository.GetUserByUsername(username);
            if (user == null)
                throw new UserNotFoundException(username);
            return user;
        }

        public IEnumerable<Booking> GetBookingByName(string username)
        {
            return _bookingRepository.GetBookingByName(username);
        }

        public object GetSeats(int id)
        {
            var seatsInfo = _bookingRepository.GetSeats(id);
            if (seatsInfo == null)
                throw new EventsNotFoundException(id);
            return seatsInfo;
        }

        public bool IsSeatAvailable(int eventId, int requestedSeats)
        {
            var ev = _bookingRepository.GetEventById(eventId);
            if (ev == null)
                throw new EventsNotFoundException(eventId);

            return ev.TotalSeats >= requestedSeats;
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
            var payment = _paymentRepository.GetPaymentByBookingId(bookingId);
            if (payment == null)
                throw new BookingNotFoundException($"Payment not found for booking ID {bookingId}.");

            return payment;
        }


        // Put
        public void UpdateBooking(Booking booking)
        {
            if (_bookingRepository.GetBookingById(booking.BookingId) == null)
                throw new BookingNotFoundException(booking.BookingId);

            _bookingRepository.UpdateBooking(booking);
        }

        public int UpdateCompletedBookings()
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            var bookingsToUpdate = _bookingRepository.GetPendingBookingsWithEvents()
                .Where(b => b.Event != null && (b.Event.EventDate < today ||
                               (b.Event.EventDate == today && b.Event.EndTime <= currentTime)))
                .ToList();


            foreach (var booking in bookingsToUpdate)
            {
                booking.Status = "Completed";
            }

            return _bookingRepository.SaveUpdatedBookings(bookingsToUpdate);
        }

        public void UpdateBooking(int id, UpdateBookingDto bookingDto)

        {
            var existingBooking = _bookingRepository.GetBookingById(id);

            if (existingBooking == null)

                throw new BookingNotFoundException(id);

            int seatChange = bookingDto.SelectedSeats - existingBooking.SelectedSeats;

            var ev = _bookingRepository.GetEventById(existingBooking.EventId);

            if (ev == null)

                throw new EventsNotFoundException(existingBooking.EventId);

            if (ev.TotalSeats < seatChange)

                throw new SeatsUnavailableException(ev.EventID, bookingDto.SelectedSeats, ev.TotalSeats + existingBooking.SelectedSeats);


            existingBooking.SelectedSeats = bookingDto.SelectedSeats;

            ev.TotalSeats -= seatChange;

            _bookingRepository.UpdateEventSeats(ev);

            _bookingRepository.UpdateBooking(existingBooking);

        }


        // Delete
        public int DeleteBooking(int id)
        {
            var result = _bookingRepository.DeleteBooking(id);

            if (result == 0)
                throw new BookingNotFoundException(id);

            return result;
        }
    }
}







//using Event_Management.DTOs;
//using Event_Management.Models;
//using Event_Management.Repository;
//using Event_Management.Data;

//namespace Event_Management.Services
//{
//    public class BookingService : IBookingService
//    {
//        private readonly IBookingRepository _bookingRepository;
//        private readonly IPaymentRepository _paymentRepository;
//        private readonly IEmailService _emailService;

//        public BookingService(IBookingRepository repo, IPaymentRepository paymentRepository, IEmailService emailService)
//        {
//            _bookingRepository = repo;
//            _paymentRepository = paymentRepository;
//            _emailService = emailService;
//        }





//        //Post
//        public BookingSummary AddBooking(int selectedSeats, string userName, int eventId)
//        {
//            var ev = _bookingRepository.GetEventById(eventId);
//            if (ev == null || selectedSeats > ev.TotalSeats)
//                throw new Exception("Invalid event or seat count.");

//            var user = _bookingRepository.GetUserByUsername(userName);
//            if (user == null)
//                throw new Exception("User not found.");

//            var booking = new Booking
//            {
//                EventId = ev.EventID,
//                UserId = user.UserId,
//                SelectedSeats = selectedSeats,
//                BookingDate = DateTime.Now,
//                Status = "Confirmed"
//            };

//            ev.TotalSeats -= selectedSeats;
//            _bookingRepository.UpdateEventSeats(ev);
//            _bookingRepository.AddBooking(booking);

//            decimal amount = ev.PricePerTicket * selectedSeats;
//            var payment = new Payment
//            {
//                EventID = ev.EventID,
//                BookingId = booking.BookingId,
//                Amount = amount,
//                //TotalAmount = amount,
//                PaymentDate = DateTime.Now,
//                PaymentMethod = "Credit Card",
//                Status = "Completed"
//            };

//            _paymentRepository.AddPayment(payment);

//            // Send Confirmation Email
//            var subject = "Booking Confirmation";
//            var body = $"Hi {user.UserName},\n\nYour booking for '{ev.EventName}' on {ev.EventDate} at {ev.EventTime} is confirmed.\n\nThank you!";
//            _emailService.SendEmailAsync(user.Email, subject, body);

//            return new BookingSummary
//            {
//                EventName = ev.EventName,
//                Location = ev.Location,
//                Time = ev.EventTime,
//                PricePerTicket = ev.PricePerTicket,
//                SelectedSeats = selectedSeats,
//                TotalAmount = amount
//            };
//        }

//        //Get
//        public IEnumerable<Booking> GetAllBookings()
//        {
//            return _bookingRepository.GetAllBookings();
//        }
//        public Booking GetBookingById(int id)
//        {
//            return _bookingRepository.GetBookingById(id);
//        }

//        public User GetUserByUsername(string username)
//        {
//            return _bookingRepository.GetUserByUsername(username);
//        }

//        public IEnumerable<Booking> GetBookingByName(string username)
//        {
//            return _bookingRepository.GetBookingByName(username);
//        }

//        public object GetSeats(int id)
//        {
//            return _bookingRepository.GetSeats(id);
//        }

//        public bool IsSeatAvailable(int eventId, int requestedSeats)
//        {
//            var ev = _bookingRepository.GetEventById(eventId);
//            return ev != null && ev.TotalSeats >= requestedSeats;
//        }

//        public IEnumerable<Booking> GetBookingsByEvent(int eventId)
//        {
//            return _bookingRepository.GetBookingsByEvent(eventId);
//        }

//        public IEnumerable<Event> GetTopBookedEvents(int count)
//        {
//            return _bookingRepository.GetTopBookedEvents(count);
//        }
//        public Payment GetPaymentByBookingId(int bookingId)
//        {
//            return _paymentRepository.GetPaymentByBookingId(bookingId);
//        }


//        //Put
//        public void UpdateBooking(Booking booking)
//        {
//            _bookingRepository.UpdateBooking(booking);
//        }
//        public int UpdateCompletedBookings()
//        {
//            var now = DateTime.Now;
//            var today = DateOnly.FromDateTime(now);
//            var currentTime = TimeOnly.FromDateTime(now);

//            var bookingsToUpdate = _bookingRepository.GetPendingBookingsWithEvents()
//                .Where(b => b.Event.EventDate < today ||
//                           (b.Event.EventDate == today && b.Event.EndTime <= currentTime))
//                .ToList();

//            foreach (var booking in bookingsToUpdate)
//            {
//                booking.Status = "Completed";
//            }

//            return _bookingRepository.SaveUpdatedBookings(bookingsToUpdate);
//        }

//        public int UpdateBooking(int id, Booking booking)
//        {
//            return _bookingRepository.UpdateBooking(id, booking);
//        }


//        //Delete
//        public int DeleteBooking(int id)
//        {
//            return _bookingRepository.DeleteBooking(id);
//        }
//    }
//}
