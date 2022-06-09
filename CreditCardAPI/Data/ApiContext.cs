using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasNoKey();
            modelBuilder.Entity<CreditCard>().HasNoKey();
        }
    }
}
