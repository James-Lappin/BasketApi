using Basket.Client;
using Basket.Domain.Models.Basket;
using Basket.Domain.Models.Domain;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Basket.Tests.Client
{
    [TestFixture]
    public class ClientTests
    {
        private IBasketClient _sut;

        [SetUp]
        public void SetUp()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            var testServer = new TestServer(builder);
            var httpClient = testServer.CreateClient();
            _sut = new BasketClient(httpClient);
        }

        [Test]
        public async Task CreateBasket()
        {
            // arrange
            const long customerId = 6L;
            var basketToCreate = new CreateBasketModel()
            {
                CustomerId = customerId
            };

            // act
            var basket = await _sut.CreateBasket(basketToCreate);

            // assert
            Assert.That(basket, Is.Not.Null);
            Assert.That(basket.Id, Is.GreaterThan(0));
            Assert.That(basket.CustomerId, Is.EqualTo(customerId));
        }

        [Test]
        public async Task GetBasket()
        {
            // arrange
            var basket = await CreateBasketWithItems();

            // act
            var actual = await _sut.GetBasket(basket.Id);

            // assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.GreaterThan(0));
            Assert.That(actual.BasketItems, Is.Not.Empty);
        }

        [Test]
        public async Task AddItemToBasket()
        {
            // arrange
            var basket = await CreateBasketWithItems();

            // act
            var actual = await AddItemToBasket(basket,10328, 5);

            // assert
            Assert.That(actual, Is.Not.Null);
            AssertItemAdded(actual, 10328, 5);
        }

        [Test]
        public async Task AddMultipleItemsToBasket()
        {
            // arrange
            var basket = await CreateBasketWithItems();

            await AddItemToBasket(basket,10328, 5);
            
            // act
            var actual = await AddItemToBasket(basket,81l, 2);

            // assert
            AssertItemAdded(actual, 81, 2);
            AssertItemAdded(actual, 10328, 5);
        }

        [Test]
        public async Task RemoveItem()
        {
            // arrange
            var basket = await CreateBasketWithItems();

            // act
            var actual = await _sut.RemoveItem(basket.Id, basket.BasketItems.First().ProductId);

            // assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.BasketItems, Is.Empty);
        }

        [Test]
        public async Task ClearBasket()
        {
            // arrange
            var basket = await CreateBasketWithItems();

            // act
            var actual = await _sut.ClearBasket(basket.Id);

            // assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.BasketItems, Is.Empty);
        }

        private async Task<BasketOfItems> CreateBasketWithItems()
        {
            const long customerId = 6L;
            var basketToCreate = new CreateBasketModel()
            {
                CustomerId = customerId
            };

            // create the basket
            var basket = await _sut.CreateBasket(basketToCreate);

            var productId = 999L;
            var quantity = 1;
            var model = new UpdateBasketModel()
            {
                BasketId = basket.Id,
                ProductId = productId,
                Quantity = quantity
            };

            // update the basket with items
            var updatedBasket = await _sut.AddItemToBasket(model);

            return updatedBasket;
        }
        
        private async Task<BasketOfItems> AddItemToBasket(BasketOfItems basket, long productId = 10328L, int quantity = 5)
        {
            return await _sut.AddItemToBasket(new UpdateBasketModel()
            {
                BasketId = basket.Id,
                ProductId = productId,
                Quantity = quantity
            });
        }
        
        private static void AssertItemAdded(BasketOfItems basket, long productId, int quantity)
        {
            var item = basket.BasketItems.FirstOrDefault(x => x.ProductId.Equals(productId));
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Id, Is.GreaterThan(0));
            Assert.That(item.Quantity, Is.EqualTo(quantity));
        }

    }
}
