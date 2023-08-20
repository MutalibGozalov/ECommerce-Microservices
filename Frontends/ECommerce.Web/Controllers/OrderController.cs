using ECommerce.Web.Models.Order;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerce.web.Controllers;
public class OrderController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public OrderController(ICartService cartService, IOrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }


    public async Task<IActionResult> Checkout()
    {
        var cart = await _cartService.Get();
        var addresses = new List<object>(){
            new {Id = 1, Address = "Address 1"},
            new {Id = 2, Address = "Address 2"},
            new {Id = 3, Address = "Address 3"}
        };
        var creaditCarts = new List<object>(){
            new {Id = 1, CardNumber = "aaaaaaa 111111"},
            new {Id = 2, CardNumber = "bbbbbbb 222222"},
            new {Id = 3, CardNumber = "ccccccc 333333"}
        };
        var shippings = new List<object>(){
            new {Id = 1004, ShippingName = "Shipping 1"},
            new {Id = 1006, ShippingName = "Shipping 2"},
            new {Id = 1007, ShippingName = "Shipping 3"}
        };

        ViewBag.addressList = new SelectList(addresses, "Id", "Address");
        ViewBag.creaditCartList = new SelectList(creaditCarts, "Id", "CardNumber");
        ViewBag.shippingList = new SelectList(shippings, "Id", "ShippingName");
        ViewBag.cart = cart;
        return View(new CheckOutInfoInput());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckOutInfoInput checkOutInfoInput)
    {
        var orderStatus = await _orderService.CreateOrder(checkOutInfoInput);

        if (orderStatus.IsSuccessfull is false)
        {
            var cart = await _cartService.Get();

            ViewBag.cart = cart;

            ViewBag.error = orderStatus.Error;

            return View();
        }

        return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = orderStatus.Id});
    }

    public IActionResult SuccessfulCheckout(int orderId)
    {
        ViewBag.orderId = orderId;
        return View();
    }
}