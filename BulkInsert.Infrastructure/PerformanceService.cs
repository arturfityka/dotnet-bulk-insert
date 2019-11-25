using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoFixture;
using BulkInsert.Core;

namespace BulkInsert.Infrastructure 
{
    public class PerformanceService : IPerformanceService
    {
        public async Task<IEnumerable<string>> CompareAsync(IEnumerable<IPaymentRepository> paymentRepositories)
        {
            var result = new List<string>();
            
            var fixture = new Fixture();
            var payments = fixture.CreateMany<Payment>(10000);

            foreach (var repository in paymentRepositories)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                
                await repository.AddAsync(payments);
                
                stopwatch.Stop();
                
                await repository.ClearAsync();
                result.Add($"{repository.GetType().Name} --- {stopwatch.Elapsed:c}");
            }

            return result;
        }
    }

    public interface IPerformanceService
    {
        Task<IEnumerable<string>> CompareAsync(IEnumerable<IPaymentRepository> paymentRepositories);
    }
}