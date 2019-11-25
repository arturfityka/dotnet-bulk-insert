using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Core;
using BulkInsert.Infrastructure;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Infrastructure.Repositories;
using Newtonsoft.Json;

namespace BulkInsert.API
{
    public static class Program
    {
        public static async Task Main()
        {
            var dbContext = new BulkInsertContext();
            var paymentRepositories = new List<IPaymentRepository>()
            {
                // new EFDummyAddRepository(dbContext),
                new EFAddRepository(dbContext),
                new EFAddRangeRepository(dbContext),
                new SqlBulkCopyRepository(dbContext),
                // new BulkInsertRepository(dbContext)
            };
            var performanceService = new PerformanceService();

            var result = await performanceService.CompareAsync(paymentRepositories);
            
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.Read();
        }
    }
}
