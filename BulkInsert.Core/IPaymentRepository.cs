using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulkInsert.Core 
{
    public interface IPaymentRepository
    {
        Task AddAsync(IEnumerable<Payment> payments);
        Task ClearAsync();
    }
}
