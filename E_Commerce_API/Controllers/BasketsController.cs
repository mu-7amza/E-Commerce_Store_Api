using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketService _basketService;
        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct)
        {
            var result = await _basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken ct)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket, ct);
            return ToActionResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var result = await _basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
