using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public BasketRepository _basketRepository { get; }

        public BasketController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}", Name = "GetBasket")]
        [ProducesResponseType(200, Type = typeof(BasketOfItems))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Get(long id)
        {
            var basket = await _basketRepository.GetBasketById(id);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpGet(Name = "GetBaskets")]
        [ProducesResponseType(200, Type = typeof(BasketOfItems[]))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Get(int page = 0, int pageSize = 10)
        {
            var baskets = await _basketRepository.GetBaskets(page, pageSize);
            if (baskets == null || !baskets.Any())
            {
                return NotFound();
            }

            return Ok(baskets);
        }

        // Create
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BasketOfItems))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BasketOfItems>> Post([FromBody] long customerId)
        {
            var basket = new BasketOfItems(customerId);
            await _basketRepository.CreateBasketAsync(basket);

            return CreatedAtRoute("GetBasket", new { id = basket.Id }, basket);
        }

        [HttpPost("/api/v1/[controller]/clearbasket/{id}")]
        [ProducesResponseType(200, Type = typeof(BasketOfItems))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> ClearBasket(long id)
        {
            var basket = await _basketRepository.GetBasketById(id);
            if (basket == null)
            {
                return NotFound();
            }

            basket.ClearBasket();
            await _basketRepository.UpdateBasket(basket);

            return Ok(basket);
        }

        // update
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BasketOfItems))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Put([FromBody] long basketId, [FromBody] long productId, [FromBody] int quantity)
        {
            var basket = await _basketRepository.GetBasketById(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            basket.AddUpdateOrRemoveItem(productId, quantity);
            await _basketRepository.UpdateBasket(basket);

            return Ok(basket);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _basketRepository.DeleteBasket(id);
            return NoContent();
        }
    }
}
