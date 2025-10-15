using Event_Management.Models;
using Event_Management.Repository;
using System.Runtime.CompilerServices;

namespace Event_Management.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void AddPayment(Payment payment)
        {
            _paymentRepository.AddPayment(payment);
        }

        public Payment GetPaymentByBookingId(int paymentId)
        {
            return _paymentRepository.GetPaymentByBookingId(paymentId);
        }
    }
}
