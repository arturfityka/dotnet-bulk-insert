using System.Collections.Generic;
using System.Threading.Tasks;
using BulkInsert.Kernel;
using BulkInsert.Kernel.Repositories;

namespace BulkInsert.Application 
{
    public interface IPerformanceService
    {
        Task<IEnumerable<string>> CompareAsync(ICollection<IPaymentRepository> paymentRepositories);
    }
}