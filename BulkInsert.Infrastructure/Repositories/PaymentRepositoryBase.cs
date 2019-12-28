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
        public BulkInsertContext DbContext { get; }
        
        public abstract string Name { get; }

        protected PaymentRepositoryBase(BulkInsertContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract Task AddAsync(IEnumerable<Payment> payments);

        public async Task ClearAsync()
            => await DbContext.Database.ExecuteSqlRawAsync($"DELETE FROM [{nameof(DbContext.Payments)}]");
    }
}
