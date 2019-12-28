using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Application;
using BulkInsert.Infrastructure.Repositories;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel.Repositories;
using System.Text.Json;
using BulkInsert.Infrastructure.Repositories.Decorators;

namespace BulkInsert.API
{
    public static class Program
    {
        public static async Task Main()
        {
            var dbContext = new BulkInsertContext();
            
            var paymentRepositories = new List<IPaymentRepository>()
            {
                new EFDummyAddRepository(dbContext),
                new EFAddRepository(dbContext),
                new EFAddRangeRepository(dbContext),
                new SqlBulkCopyRepository(dbContext),
                new AutoDetectChangesDisabledPaymentRepositoryDecorator(new EFDummyAddRepository(dbContext)),
                new AutoDetectChangesDisabledPaymentRepositoryDecorator(new EFAddRepository(dbContext)),
                new AutoDetectChangesDisabledPaymentRepositoryDecorator(new EFAddRangeRepository(dbContext)),
                new AutoDetectChangesDisabledPaymentRepositoryDecorator(new SqlBulkCopyRepository(dbContext)),
                // new BulkInsertRepository(dbContext)
            };
            
            IPerformanceService performanceService = new PerformanceService();

            var result = await performanceService.CompareAsync(paymentRepositories);

            var serializedResult
                = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }); 
            Console.WriteLine(serializedResult);
            Console.Read();
        }
    }
}
