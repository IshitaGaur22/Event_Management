using Event_Management.Models;
using Event_Management.Data;

namespace Event_Management.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Event_ManagementContext _context;
        public PaymentRepository(Event_ManagementContext context)
        {
            _context = context;
        }
        public void AddPayment(Payment payment)
        {
            _context.Payment.Add(payment);
            _context.SaveChanges();
        }

        public Payment GetPaymentByBookingId(int bookingId)
        {
            return _context.Payment.FirstOrDefault(p => p.BookingId == bookingId);
        }
    }
}
