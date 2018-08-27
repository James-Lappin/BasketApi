using Basket.Api.Models;
using Basket.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Repositories
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
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