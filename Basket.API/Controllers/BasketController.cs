using Basket.API.Entities;
using Basket.API.gRPCServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Basket.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountgRPCService _discountgRPCService;

        public BasketController(IBasketRepository repository, DiscountgRPCService discountgRPCService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountgRPCService = discountgRPCService;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await _discountgRPCService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _repository.UpdateBasket(cart));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }
    }
}
