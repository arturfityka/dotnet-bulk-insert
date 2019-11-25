using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkInsert.Core;
using BulkInsert.Infrastructure.EntityFramework;
using Fare;
using Microsoft.EntityFrameworkCore;

namespace BulkInsert.Infrastructure.Repositories 
{
    public class EFAddRangeRepository : PaymentRepository
    {
        public EFAddRangeRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            await _dbContext.Payments.AddRangeAsync(payments);

            await _dbContext.SaveChangesAsync();
        }
    }
    
    public class BulkInsertRepository : PaymentRepository
    {
        public BulkInsertRepository(BulkInsertContext dbContext) : base(dbContext) {}

        public override async Task AddAsync(IEnumerable<Payment> payments)
            => _dbContext.BulkInsert(payments);
    }
}