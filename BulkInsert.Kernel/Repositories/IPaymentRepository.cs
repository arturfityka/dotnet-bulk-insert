using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulkInsert.Kernel.Repositories 
{
    public interface IPaymentRepository
    {
        string Name { get; }
        
        Task AddAsync(IEnumerable<Payment> payments);
        Task ClearAsync();
    }
}
