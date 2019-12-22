using System;

namespace BulkInsert.Kernel
{
    public class Payment
    {
        public Guid Id { get; }

        public Payment()
        {
            Id = Guid.NewGuid();
        }
    }
}
