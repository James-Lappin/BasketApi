using Basket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Repositories
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<BasketOfItems> Baskets { get; set; }
    }
}