using System;
using Basket.Api.Models;
using NUnit.Framework;

namespace Basket.UnitTests.Services
{
    public class BasketItemTests
    {
        [Test]
        public void CreateInvalidQuatityBasketItem()
        {
            // arrange
            // act
            var ex = Assert.Throws<ArgumentException>(() => new BasketItem(1, 0));
            
            // assert
            Assert.That(ex.Message, Is.EqualTo("quantity"));
        }

        [Test]
        public void CreateInvalidProductBasketItem()
        {
            // arrange
            // act
            var ex = Assert.Throws<ArgumentException>(() => new BasketItem(0, 1));
            
            // assert
            Assert.That(ex.Message, Is.EqualTo("productId"));
        }

        [TestCase(100, 1)]
        [TestCase(12, 4)]
        public void CreateBasketItem(long productId, int quantity)
        {
            // arrange
            // act
            var sut = new BasketItem(productId, quantity);

            // assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.ProductId, Is.EqualTo(productId));
            Assert.That(sut.Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void UpdateBasket()
        {
            // arrange
            var sut = new BasketItem(1, 3);

            // act
            sut.UpdateQuantity(6);

            // assert
            Assert.That(sut.ProductId, Is.EqualTo(1));
            Assert.That(sut.Quantity, Is.EqualTo(6));
        }

        [Test]
        public void UpdateBasketWithInvalidQuantity()
        {
            // arrange
            var sut = new BasketItem(1, 3);

            // act
            var ex = Assert.Throws<ArgumentException>(() => sut.UpdateQuantity(0));

            // assert
            Assert.That(ex.Message, Is.EqualTo("newQuantity"));
        }
    }
}