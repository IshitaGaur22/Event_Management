using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IPaymentService
    {
        void AddPayment(Payment payment);
        Payment GetPaymentByBookingId(int bookingId);
    }
}
