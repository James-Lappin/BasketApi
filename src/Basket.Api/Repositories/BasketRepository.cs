using System;
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

        public async Task<BasketOfItems> GetBasket(long basketId)
        {
            return await _context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basketId));
        }

        public async Task DeleteBasket(long basketId)
        {
            var dbBasket = await _context.Baskets.FirstOrDefaultAsync(x => x.Id.Equals(basketId));
            _context.Baskets.Remove(dbBasket);

            await _context.SaveChangesAsync();
        }
    }
}