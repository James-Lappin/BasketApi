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

        public DbSet<BasketOfItems> Baskets { get; set; }
    }
}