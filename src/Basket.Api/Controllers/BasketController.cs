using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    /// <summary>
    /// Actions to create, modify and delete baskets.
    /// </summary>
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

        /// <summary>
        /// Gets a specific Basket.
        /// </summary>
        /// <param name="id">Id of basket to get</param>
        /// <response code="404">Returns basket</response>
        /// <response code="404">Basket could not be found</response>
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

        /// <summary>
        /// Gets all baskets by page and page size.
        /// </summary>
        /// <param name="page">Page to retrieve</param>
        /// <param name="pageSize">Amount of Baskets to retrieve</param>
        /// <response code="200">Returns list of baskets found</response>
        /// <response code="404">Baskets could not be found</response>
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


        /// <summary>
        /// Creates a basket for the customer id supplied.
        /// </summary>
        /// <param name="customerId">Id of customer to create basket for</param>
        /// <response code="201">Basket has been created</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BasketOfItems))]
        public async Task<ActionResult<BasketOfItems>> Post([FromBody] long customerId)
        {
            var basket = new BasketOfItems(customerId);
            await _basketRepository.CreateBasketAsync(basket);

            return CreatedAtRoute("GetBasket", new { id = basket.Id }, basket);
        }

        /// <summary>
        /// Removes all items from basket.
        /// </summary>
        /// <param name="id">id of basket to clear</param>
        /// <response code="200">Basket has successfully been cleared</response>
        /// <response code="404">Basket could not be found</response>
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

        /// <summary>
        /// Updates basket.
        /// </summary>
        /// <remarks>
        /// This can be used to add a new item to the basket, update an existing item or remove an item from the basket.
        /// </remarks>
        /// <param name="basketId">Id of the basket to be updated.</param>
        /// <param name="productId">Id of the product to be added, updated or removed.</param>
        /// <param name="quantity">The quatity of product to be in the basket. A quantity of 0 will remove the item from the basket</param>
        /// <response code="200">Successfully updated the basket</response>
        /// <response code="404">Basket could not be found</response>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BasketOfItems))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Put([FromForm] long basketId, [FromForm] long productId, [FromForm] int quantity)
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

        /// <summary>
        /// Deletes a specific Basket.
        /// </summary>
        /// <param name="id">id of the basket to delete</param>
        /// <response code="204">Confirms the basket is no longer there</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            await _basketRepository.DeleteBasket(id);
            return NoContent();
        }
    }
}
