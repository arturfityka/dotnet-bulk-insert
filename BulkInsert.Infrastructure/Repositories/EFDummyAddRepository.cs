using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories 
{
    public class EFDummyAddRepository : PaymentRepositoryBase
    {
        public override string Name { get; } = nameof(EFDummyAddRepository);
        
        public EFDummyAddRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            foreach (var payment in payments)
            {
                await DbContext.Payments.AddAsync(payment);
                    
                await DbContext.SaveChangesAsync();
            }
        }
    }
}