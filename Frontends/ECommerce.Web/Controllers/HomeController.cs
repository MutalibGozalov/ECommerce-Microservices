using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using ECommerce.Web.Exceptions;

namespace ECommerce.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogService _catalogService;

    public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllProductsAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorFeatures = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (errorFeatures is not null && errorFeatures.Error is UnAuthorizeException)
        {
            return RedirectToAction(nameof(AuthController.Logout), "Auth");
        }
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
