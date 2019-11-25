using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Core;

namespace BulkInsert.Infrastructure
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        
        public async Task SaveAsync(IEnumerable<Payment> payments)
            => await _paymentRepository.AddAsync(payments);

        public async Task DeleteAsync()
            => await _paymentRepository.ClearAsync();
    }
}
