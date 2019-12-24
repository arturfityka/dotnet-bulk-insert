using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Kernel
{
    public class PerformanceMeasurementFacility
    {
        private static readonly TimeSpan PoorPerformanceBoundary = TimeSpan.FromMilliseconds(100);
        private static readonly IDictionary<int, TimeSpan> Results = new Dictionary<int, TimeSpan>();
        
        private readonly IPaymentRepository _repository;

        public PerformanceMeasurementFacility(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task MeasureAsync(Payment[] payments)
        {
            if (IsPoorPerformanceExpected(payments.Length))
            {
                return; // TODO add result with info about poor performance
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _repository.AddAsync(payments);

            stopwatch.Stop();

            await _repository.ClearAsync();

            Results.Add(payments.Length, stopwatch.Elapsed);
        }

        private static bool IsPoorPerformanceExpected(int sampleSize)
        {
            if (Results.Count == 0)
            {
                return false;
            }

            return Results
                .Skip(1)
                .Where(x => x.Key <= sampleSize)
                .Any(x => x.Value > PoorPerformanceBoundary);
        }
    }
}
