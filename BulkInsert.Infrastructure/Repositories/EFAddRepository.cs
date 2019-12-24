using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories
{
    public class EFAddRepository : PaymentRepositoryBase
    {
        public override string Name { get; } = nameof(EFAddRepository);
        
        public EFAddRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            foreach (var payment in payments)
            {
                await _dbContext.AddAsync(payment);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
