using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Repositories
{
    public class BasketRepository
    {
        private ApiContext _context { get; set; }
        public BasketRepository(ApiContext apiContext)
        {
            _context = apiContext;
        }

        public async Task CreateBasketAsync(BasketOfItems basket)
        {
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasket(BasketOfItems basket)
        {
            var dbBasket = await _context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basket.Id));
            if (basket == null)
            {
                throw new ArgumentException("Couldnt find basket");
            }

            dbBasket = basket;
            await _context.SaveChangesAsync();
        }

        public async Task<BasketOfItems> GetBasketById(long basketId)
        {
            return await _context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basketId));
        }

        public async Task<BasketOfItems[]> GetBaskets(int page = 0, int pageSize = 10)
        {
            return await _context.Baskets
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .ToArrayAsync();
        }

        public async Task DeleteBasket(long basketId)
        {
            var dbBasket = await _context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basketId));
            if (dbBasket == null){
                // This I'm a little unsure about. I dont know whether I should be telling the caller that it never existed or not
                // In theory it's the same outcome.
                return;
            }
            
            _context.Baskets.Remove(dbBasket);
            await _context.SaveChangesAsync();
        }
    }
}