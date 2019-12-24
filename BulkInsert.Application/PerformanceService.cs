using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using BulkInsert.Application.DTOs;
using BulkInsert.Kernel;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Application 
{
    public class PerformanceService : IPerformanceService
    {
        private const int SamplesMaxNumber = 20;
        private const int SampleSizeGrowthFactor = 2;
        
        private static readonly Fixture Fixture = new Fixture();
        private static readonly int MaxSampleSize 
            = Convert.ToInt32(Math.Min(
                Convert.ToDouble(int.MaxValue), 
                Math.Pow(SampleSizeGrowthFactor, SamplesMaxNumber)
            ));

        public async Task<IEnumerable<PerformanceComparisonDto>> CompareAsync(
            ICollection<IPaymentRepository> paymentRepositories
        )
        {
            var measurementFacilities 
                = paymentRepositories.Select(repository => new PerformanceMeasurementFacility(repository)).ToArray();
            
            for (var sampleSize = 1; sampleSize < MaxSampleSize; sampleSize *= SampleSizeGrowthFactor)
            {
                await CompareAsync(measurementFacilities, sampleSize);
            }

            return measurementFacilities.Select(facility => new PerformanceComparisonDto(facility.Results));
        }

        private static async Task CompareAsync(IEnumerable<PerformanceMeasurementFacility> measurementFacilities, int sampleSize)
        {
            var payments = Fixture.CreateMany<Payment>(sampleSize).ToArray();

            foreach (var measurementFacility in measurementFacilities)
            {
                await measurementFacility.MeasureAsync(payments);
            }
        }
    }
}