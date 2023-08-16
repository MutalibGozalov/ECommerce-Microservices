using ECommerce.Shared.Services;
using ECommerce.Web.Models.Catalog;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerce.Web.Controllers;
[Authorize]
public class ProductController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public ProductController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
    {
        _catalogService = catalogService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _catalogService.GetAllProductsAsync();
        return View(data);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        if (ModelState.IsValid is false)
        {
            return View();
        }

        productCreateInput.StoreId = 1;

        await _catalogService.CreateProductAsync(productCreateInput);

        return RedirectToAction("Index");
    }
}