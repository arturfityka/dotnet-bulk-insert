using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories.Decorators
{
    public class AutoDetectChangesDisabledPaymentRepositoryDecorator : PaymentRepositoryDecorator
    {
        public AutoDetectChangesDisabledPaymentRepositoryDecorator(PaymentRepositoryBase paymentRepository) 
            : base(paymentRepository) {}

        public override string Name
            => $"{base.Name} with {nameof(AutoDetectChangesDisabledPaymentRepositoryDecorator)}";

        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            PaymentRepository.DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            
            await base.AddAsync(payments);
            
            PaymentRepository.DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}
