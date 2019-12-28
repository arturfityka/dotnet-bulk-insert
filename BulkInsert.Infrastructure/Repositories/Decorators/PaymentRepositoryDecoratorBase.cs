using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories.Decorators
{
    public abstract class PaymentRepositoryDecorator : PaymentRepositoryBase
    {
        public override string Name
            => PaymentRepository.Name;
        
        protected PaymentRepositoryBase PaymentRepository { get; }

        protected PaymentRepositoryDecorator(PaymentRepositoryBase paymentRepository) 
            : base(paymentRepository.DbContext)
        {
            PaymentRepository = paymentRepository;
        }
        
        public override async Task AddAsync(IEnumerable<Payment> payments)
            => await PaymentRepository.AddAsync(payments);
    }
}
