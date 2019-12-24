using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Application.DTOs;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Application 
{
    public interface IPerformanceService
    {
        Task<IEnumerable<PerformanceComparisonDto>> CompareAsync(
            ICollection<IPaymentRepository> paymentRepositories
        );
    }
}