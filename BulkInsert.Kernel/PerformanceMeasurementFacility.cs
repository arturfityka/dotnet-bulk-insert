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
        private static readonly ICollection<PerformanceMeasurementResult> Results 
            = new List<PerformanceMeasurementResult>();
        
        private readonly IPaymentRepository _repository;

        public PerformanceMeasurementFacility(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task MeasureAsync(Payment[] payments)
        {
            if (IsPoorPerformanceExpected(payments.Length))
            {
                AddResultWithExpectedPoorPerformance(payments.Length);
                
                return;
            }

            var value = await MeasureAndClearAsync(payments);

            AddResult(payments.Length, value);
        }

        private static bool IsPoorPerformanceExpected(int sampleSize)
        {
            if (Results.Count == 0)
            {
                return false;
            }

            return Results
               .Skip(1)
               .Where(result => result.SampleSize <= sampleSize)
               .Any(result => result.Value > PoorPerformanceBoundary);
        }

        private void AddResultWithExpectedPoorPerformance(int sampleSize)
        {
            const string message = "Poor performance is expected.";
            var result = new PerformanceMeasurementResult(
                nameof(_repository),
                sampleSize,
                message
            );
            
            Results.Add(result);
        }

        private async Task<TimeSpan> MeasureAndClearAsync(IEnumerable<Payment> payments)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _repository.AddAsync(payments);

            stopwatch.Stop();

            await _repository.ClearAsync();

            return stopwatch.Elapsed;
        }

        private void AddResult(int sampleSize, TimeSpan value)
        {
            var result = new PerformanceMeasurementResult(
                nameof(_repository),
                sampleSize,
                value
            );
            
            Results.Add(result);
        }
    }
}
