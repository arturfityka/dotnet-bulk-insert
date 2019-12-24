using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories 
{
    public class EFAddRangeRepository : PaymentRepositoryBase
    {
        public override string Name { get; } = nameof(EFAddRangeRepository);
        
        public EFAddRangeRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            await _dbContext.Payments.AddRangeAsync(payments);

            await _dbContext.SaveChangesAsync();
        }
    }
}