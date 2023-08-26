using ECommerce.Web.Models.Cart;
using ECommerce.Web.Models.Discount;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;
[Authorize]
public class CartController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;

    public CartController(ICatalogService catalogService, ICartService cartService)
    {
        _catalogService = catalogService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _cartService.Get());
    }

    public async Task<IActionResult> Cart()
    {
        return View(await _cartService.Get());
    }


    public async Task<IActionResult> AddItemToCart(string productId)
    {
        var product = await _catalogService.GetProductByIdAsync(productId);
        await _cartService.AddItemToCart(new CartItemViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.DisplayPrice,
            Quantity = 1
        });
        return RedirectToAction(nameof(Cart));
    }

    public async Task<IActionResult> RemoveCartItem(string productId)
    {
        await _cartService.RevokeCartItem(productId);
        return RedirectToAction(nameof(Cart));
    }

    public async Task<IActionResult> DeleteCartItem(string productId)
    {
        await _cartService.DeleteCartItem(productId);
        return RedirectToAction(nameof(Cart));
    }

    public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
    {
        if (ModelState.IsValid is false)
        {
            TempData["discountError"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).First();
            return RedirectToAction(nameof(Cart));
        }
        var discountStatus = await _cartService.ApplyDiscount(discountApplyInput.Code);
        TempData["discountstatus"] = discountStatus;
        return RedirectToAction(nameof(Cart));
    }

    public async Task<IActionResult> CancelDiscount()
    {
        await _cartService.CancelDiscount();
        return RedirectToAction(nameof(Cart));
    }
}