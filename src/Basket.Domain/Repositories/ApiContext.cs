using Basket.Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.Domain.Repositories
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base((DbContextOptions) options)
        {
        }

        // Used for testing
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketOfItems>().HasData(new BasketOfItems(4));
        }

        public DbSet<BasketOfItems> Baskets { get; set; }
    }
}