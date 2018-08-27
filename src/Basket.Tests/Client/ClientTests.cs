using Basket.Client;
using Basket.Domain.Models.Basket;
using Basket.Domain.Models.Domain;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Tests.Client
{
    [TestFixture]
    public class ClientTests
    {
        private IBasketClient _sut;

        [SetUp]
        public void SetUp()
        {
            // Probably gonna have to get this from somewhere
            var baseAddress = "http://localhost:55603";
            _sut = BasketClient.Create(baseAddress);
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

            var productId = 10328L;
            var quantity = 5;
            var model = new UpdateBasketModel()
            {
                BasketId = basket.Id,
                ProductId = productId,
                Quantity = quantity
            };

            // act
            var actual = await _sut.AddItemToBasket(model);

            // assert
            Assert.That(actual, Is.Not.Null);
            var itemAdded = actual.BasketItems.FirstOrDefault(x => x.ProductId.Equals(productId));
            Assert.That(itemAdded, Is.Not.Null);
            Assert.That(itemAdded.Id, Is.GreaterThan(0));
            Assert.That(itemAdded.Quantity, Is.EqualTo(quantity));
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
    }
}
