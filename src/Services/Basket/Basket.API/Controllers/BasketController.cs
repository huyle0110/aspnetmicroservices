using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController: ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpGet("[action]/{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute] string userName)
        {
            var result = await _basketRepository.GetBasket(userName);
            return Ok(result ?? new ShoppingCart(userName));
        }

        [HttpDelete]
        [Route("[action]/{userName}")]

        public async Task<IActionResult> Delete([FromRoute] string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
