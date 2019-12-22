using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;

namespace BulkInsert.Infrastructure.Repositories 
{
    public class EFDummyAddRepository : PaymentRepository
    {
        public EFDummyAddRepository(BulkInsertContext dbContext) : base(dbContext) {}
            
        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            foreach (var payment in payments)
            {
                await _dbContext.Payments.AddAsync(payment);
                    
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}