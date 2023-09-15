using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Cart.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CartController : CustomBaseController
{
    private ICartService _cartService;
    private ISharedIdentityService _sharedIdentityService;

    public CartController(ICartService cartService, ISharedIdentityService sharedIdentityService)
    {
        _cartService = cartService;
        _sharedIdentityService = sharedIdentityService;

    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        return CreateActionResultInstance(await _cartService.GetCart(_sharedIdentityService.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateCart(CartDto cartDto)
    {
        cartDto.UserId = _sharedIdentityService.GetUserId;
        var response = await _cartService.SaveOrUpdate(cartDto);

        return CreateActionResultInstance(response);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCart()
    {
        return CreateActionResultInstance(await _cartService.Delete(_sharedIdentityService.GetUserId));
    }
}
//docker run --name redis-cartDb -d -p 6379:6379 redis