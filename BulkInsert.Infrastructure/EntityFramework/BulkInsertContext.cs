using BulkInsert.Kernel;
using Microsoft.EntityFrameworkCore;

namespace BulkInsert.Infrastructure.EntityFramework
{
    public class BulkInsertContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<Payment>().HasKey(x => x.Id);
    }
}
