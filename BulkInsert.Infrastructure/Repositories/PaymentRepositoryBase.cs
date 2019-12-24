using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;
using BulkInsert.Kernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BulkInsert.Infrastructure.Repositories
{
    public abstract class PaymentRepositoryBase : IPaymentRepository
    {
        public abstract string Name { get; }
        
        protected readonly BulkInsertContext _dbContext;
        
        protected PaymentRepositoryBase(BulkInsertContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract Task AddAsync(IEnumerable<Payment> payments);

        public async Task ClearAsync()
            => await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM [{_dbContext.Payments}]");
    }
}
