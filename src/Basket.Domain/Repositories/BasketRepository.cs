using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Basket.Domain.Repositories
{
    public class BasketRepository
    {
        private ApiContext Context { get; set; }
        public BasketRepository(ApiContext apiContext)
        {
            Context = apiContext;
        }

        public async Task CreateBasketAsync(BasketOfItems basket)
        {
            Context.Baskets.Add(basket);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateBasket(BasketOfItems basket)
        {
            var dbBasket = await Context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basket.Id));
            if (dbBasket == null)
            {
                throw new ArgumentException("Couldnt find basket");
            }

            dbBasket = basket;
            await Context.SaveChangesAsync();
        }

        public async Task<BasketOfItems> GetBasketById(long basketId)
        {
            return await Context.Baskets
                .Include(x=>x.BasketItems)
                .FirstOrDefaultAsync(x => x.Id.Equals(basketId));
        }

        public async Task<BasketOfItems[]> GetBaskets(int page, int pageSize)
        {
            return await Context.Baskets
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .ToArrayAsync();
        }

        public async Task DeleteBasket(long basketId)
        {
            var dbBasket = await Context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basketId));
            if (dbBasket == null){
                // This I'm a little unsure about. I dont know whether I should be telling the caller that it never existed or not
                // In theory it's the same outcome.
                return;
            }
            
            Context.Baskets.Remove(dbBasket);
            await Context.SaveChangesAsync();
        }
    }
}