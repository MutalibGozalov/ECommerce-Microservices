using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly ICatalogService _catalogService;


    public UserController(IUserService userService, IOrderService orderService, ICatalogService catalogService)
    {
        _userService = userService;
        _orderService = orderService;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();

        ViewBag.orders = orders;
        ViewBag.StoreProductList = await _catalogService.GetAllProductsAsync();


        return View("Account", await _userService.GetUser());
    }
}