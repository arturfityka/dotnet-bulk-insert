using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BulkInsert.Infrastructure.EntityFramework;
using BulkInsert.Kernel;
using Microsoft.Data.SqlClient;

namespace BulkInsert.Infrastructure.Repositories {
    public class SqlBulkCopyRepository : PaymentRepository
    {
        public SqlBulkCopyRepository(BulkInsertContext dbContext) : base(dbContext) {}
        
        public override async Task AddAsync(IEnumerable<Payment> payments)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(nameof(Payment.Id), typeof(Guid));
            
            foreach (var payment in payments)
            {
                dataTable.Rows.Add(payment.Id);
            }

            using (var sqlBulk = new SqlBulkCopy(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
            {
                sqlBulk.DestinationTableName = nameof(_dbContext.Payments);
                await sqlBulk.WriteToServerAsync(dataTable);
            }
        }
    }
}