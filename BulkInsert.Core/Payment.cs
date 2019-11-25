using System;

namespace BulkInsert.Core
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
