using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Basket.UnitTests.Repositories
{
    public class BasketRepositoryTests : IDisposable
    {
        private BasketRepository _sut;
        private ApiContext _context;

        public void Dispose()
        {
            _context?.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: $"Testing in-memory db - {Guid.NewGuid().ToString()}")
                .Options;
            _context = new ApiContext(options);
            _sut = new BasketRepository(_context);
        }

        [Test]
        public async Task CreateBasket()
        {
            // arrange
            var basket = new BasketOfItems(1);

            // act
            await _sut.CreateBasketAsync(basket);

            // assert
            var dbBasket = await  _context.Baskets.FirstOrDefaultAsync(x=>x.Id.Equals(basket.Id));
            Assert.That(dbBasket, Is.Not.Null);
        }

        [Test]
        public async Task UpdateBasket()
        {
            // arrange
            var basket = new BasketOfItems(1);
            await _sut.CreateBasketAsync(basket);

            basket.AddUpdateOrRemoveItem(1, 4);

            // act
            await _sut.UpdateBasket(basket);

            // assert
            var dbBasket = await  _context.Baskets.FirstOrDefaultAsync(x=>x.Id.Equals(basket.Id));
            Assert.That(dbBasket, Is.Not.Null);
            Assert.That(dbBasket.BasketItems, Has.Count.EqualTo(1));
            Assert.That(dbBasket.BasketItems.All(x=>x.Quantity.Equals(4)));
        }

        [Test]
        public async Task GetBasket()
        {
            // arrange
            var basket = new BasketOfItems(1);
            basket.AddUpdateOrRemoveItem(1, 3);
            await _sut.CreateBasketAsync(basket);

            // act
            var actual = await _sut.GetBasket(basket.Id);

            // assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.GreaterThan(0));
            Assert.That(actual.BasketItems, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task DeleteBasket()
        {
            // arrange
            var basket = new BasketOfItems(1);
            basket.AddUpdateOrRemoveItem(1, 3);
            await _sut.CreateBasketAsync(basket);

            // act
            await _sut.DeleteBasket(basket.Id);

            // assert
            var dbBasket = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id.Equals(basket.Id));
            Assert.That(dbBasket, Is.Null);
        }

        [Test]
        public async Task DeleteBasketWhereBasketDoesntExist()
        {
            // arrange
            const long basketId = 1L;

            // act
            await _sut.DeleteBasket(basketId);

            // assert
            var dbBasket = await _context.Baskets.FirstOrDefaultAsync(x=>x.Id.Equals(basketId));
            Assert.That(dbBasket, Is.Null);
        }
    }
}