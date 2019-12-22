using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using BulkInsert.Kernel;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Application 
{
    public class PerformanceService : IPerformanceService
    {
        private readonly Fixture _fixture;

        public PerformanceService()
        {
            _fixture = new Fixture();
        }
        
        public async Task<IEnumerable<string>> CompareAsync(ICollection<IPaymentRepository> paymentRepositories)
        {
            var testFacilities 
                = paymentRepositories.Select(x => new PerformanceMeasurementFacility(x)).ToList();
            
            for (var sampleSize = 1; sampleSize < int.MaxValue; sampleSize *= 2)
            {
                await CompareAsync(testFacilities, sampleSize);
            }
            
            return new List<string>();
        }

        private async Task CompareAsync(IEnumerable<PerformanceMeasurementFacility> testFacilities, int sample)
        {
            var payments = _fixture.CreateMany<Payment>(sample).ToArray();

            foreach (var testFacility in testFacilities)
            {
                await testFacility.MeasureAsync(payments);
            }
        }
    }
}