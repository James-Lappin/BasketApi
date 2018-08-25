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
    [ApiController]
    public class BasketController : ControllerBase
    {
        public BasketRepository _basketRepository { get; }

        public BasketController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BasketOfItems))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BasketOfItems>> Get(long id)
        {
            var basket = await _basketRepository.GetBasket(id);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        // Create
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // update
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }
    }
}
