using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories
{
    public class BulkInsertRepository : PaymentRepositoryBase
    {
        public override string Name { get; } = nameof(BulkInsertRepository);
        
        public BulkInsertRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
            => await _dbContext.BulkInsertAsync(payments);
    }
}
