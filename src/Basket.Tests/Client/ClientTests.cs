using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Basket.Api;
using Basket.Api.Basket;
using Basket.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Basket.Tests.Client
{
    [TestFixture]
    class ClientTests
    {
        private BasketClient _sut;

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
            var basketToCreate = new CreateBasketModel()
            {
                CustomerId = 6L
            };

            // act
            await _sut.Create(basketToCreate);

            // assert
        }
    }
}
