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

        [HttpGet(Name = "GetBasket")]
        [ProducesResponseType(200, Type = typeof(BasketOfItems[]))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Get(int page = 0, int pageSize = 10)
        {
            var basket = await _basketRepository.GetBaskets();
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        // Create
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BasketOfItems))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BasketOfItems>> Post([FromBody] long customerId)
        {
            var basket = new BasketOfItems(customerId);
            await _basketRepository.CreateBasketAsync(basket);

            return CreatedAtRoute("GetProduct", new { id = basket.Id }, basket);
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BasketOfItems basket)
        {
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
