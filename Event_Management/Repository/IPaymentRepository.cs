using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        Payment GetPaymentByBookingId(int bookingId);
    }
}
