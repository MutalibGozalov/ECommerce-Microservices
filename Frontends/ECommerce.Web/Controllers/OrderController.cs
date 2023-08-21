using ECommerce.Web.Models.Order;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerce.Web.Controllers;
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
            new {Id = 1, Address = "Address: Mahammad Hadi"},
            new {Id = 2, Address = "Address: Ahmadli 8th"},
            new {Id = 3, Address = "Address: Ukranian Circle"}
        };
        var creaditCarts = new List<object>(){
            new {Id = 1, CardNumber = "Visa ..3412"},
            new {Id = 2, CardNumber = "Visa ..4612"},
            new {Id = 3, CardNumber = "Master ..5342"}
        };
        var shippings = new List<object>(){
            new {Id = 1004, ShippingName = "ExpCargo"},
            new {Id = 5, ShippingName = "FedEx"},
            new {Id = 1006, ShippingName = "Wolt"},
            new {Id = 1007, ShippingName = "Mover"}
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

        /*  Synchronius request
         var orderStatus = await _orderService.CreateOrder(checkOutInfoInput); */

        // Async request
         var orderRequestStatus = await _orderService.RequestOrder(checkOutInfoInput);


        if (orderRequestStatus.IsSuccessfull is false)
        {
            var cart = await _cartService.Get();

            ViewBag.cart = cart;

            ViewBag.error = orderRequestStatus.Error;

            return View();
        }

        return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = new Random().Next(1, 1000)}); //new {orderId = orderStatus.Id}
    }

    public IActionResult SuccessfulCheckout(int orderId)
    {
        ViewBag.orderId = orderId;
        return View();
    }

    public async Task<IActionResult> OrderHistory()
    {
        return View(await _orderService.GetOrders());
    }
}