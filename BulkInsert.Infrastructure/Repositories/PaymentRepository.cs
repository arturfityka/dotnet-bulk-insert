using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Core;
using BulkInsert.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BulkInsert.Infrastructure.Repositories {
    public abstract class PaymentRepository : IPaymentRepository
    {
        protected readonly BulkInsertContext _dbContext;

        protected PaymentRepository(BulkInsertContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract Task AddAsync(IEnumerable<Payment> payments);

        public async Task ClearAsync()
            => await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Payments]");
    }
}