using System.Linq;
using Basket.Api.Models.Domain;
using NUnit.Framework;

namespace Basket.Tests.Domain
{
    public class BasketUnitTests 
    {

        [Test]
        public void CreateBasket()
        {
            // arrange
            // act 
            var sut = new BasketOfItems(1);

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Is.Empty);
        }

        [Test]
        public void AddSingleItem()
        {
            // arrange
            var sut = new BasketOfItems(1);

            // act 
            sut.AddUpdateOrRemoveItem(1, 1);

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Has.Count.EqualTo(1));
            Assert.That(sut.BasketItems.All(x => x.Quantity.Equals(1)));
        }

        [Test]
        public void AddMultipleItems()
        {
            // arrange
            var sut = new BasketOfItems(1);

            // act 
            sut.AddUpdateOrRemoveItem(1, 1);
            sut.AddUpdateOrRemoveItem(3, 1);

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Has.Count.EqualTo(2));
            Assert.That(sut.BasketItems.All(x => x.Quantity.Equals(1)));
        }

        [Test]
        public void AddSameItemMultipleTimes()
        {
            // arrange
            var sut = new BasketOfItems(1);

            // act 
            sut.AddUpdateOrRemoveItem(1, 1);
            sut.AddUpdateOrRemoveItem(1, 1);

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Has.Count.EqualTo(1));
            Assert.That(sut.BasketItems.All(x => x.Quantity.Equals(1)));
        }

        [Test]
        public void AddItemThenRemove()
        {
            // arrange
            var sut = new BasketOfItems(1);

            // act 
            sut.AddUpdateOrRemoveItem(1, 1);
            sut.AddUpdateOrRemoveItem(1, 0);

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Is.Empty);
        }

        [Test]
        public void ClearBasket()
        {
            // arrange
            var sut = new BasketOfItems(1);
            sut.AddUpdateOrRemoveItem(1, 1);
            sut.AddUpdateOrRemoveItem(2, 3);

            // act 
            sut.ClearBasket();

            //assert
            Assert.That(sut.Id, Is.GreaterThan(0));
            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(sut.BasketItems, Is.Empty);
        }
    }
}