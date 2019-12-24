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
        
        private readonly IPaymentRepository _objectUnderMeasurement;

        public PerformanceMeasurementFacility(IPaymentRepository objectUnderMeasurement)
        {
            _objectUnderMeasurement = objectUnderMeasurement;
        }

        public async Task MeasureAsync(Payment[] payments)
        {
            if (IsPoorPerformanceExpected(payments.Length))
            {
                KeepResultWithExpectedPoorPerformance(payments.Length);
                
                return;
            }

            await MeasureAndKeepResultAsync(payments);
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

        private void KeepResultWithExpectedPoorPerformance(int sampleSize)
        {
            const string message = "Poor performance is expected.";
            var result = new PerformanceMeasurementResult(
                _objectUnderMeasurement.Name,
                sampleSize,
                message
            );
            
            Results.Add(result);
        }

        private async Task MeasureAndKeepResultAsync(IReadOnlyCollection<Payment> payments)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _objectUnderMeasurement.AddAsync(payments);

            stopwatch.Stop();

            await _objectUnderMeasurement.ClearAsync();

            KeepResult(payments.Count, stopwatch.Elapsed);
        }

        private void KeepResult(int sampleSize, TimeSpan value)
        {
            var result = new PerformanceMeasurementResult(
                nameof(_objectUnderMeasurement),
                sampleSize,
                value
            );
            
            Results.Add(result);
        }
    }
}
