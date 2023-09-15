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

    public async Task<IActionResult> ProductDetail(string id)
    {
        var data = await _catalogService.GetProductByIdAsync(id);
        return View(data);
    }

    public async Task<IActionResult> Shop()
    {
        var data = await _catalogService.GetAllProductsAsync();
        return View(data);
    }
    public async Task<IActionResult> ProductsJson()
    {
        var data = await _catalogService.GetAllProductsAsync();
        return Json(data);
    }

    [Authorize(AuthenticationSchemes ="Cookies", Roles ="StoreOwner")]
    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        return View();
    }

    [Authorize(AuthenticationSchemes ="Cookies", Roles ="StoreOwner")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
    {
        // var categories = await _catalogService.GetAllCategoriesAsync();
        // ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        if (ModelState.IsValid is false)
        {
            return View();
        }

        productCreateInput.StoreId = 1;

        await _catalogService.CreateProductAsync(productCreateInput);

        return RedirectToAction(actionName:"", controllerName: "User");
    }

    [Authorize(AuthenticationSchemes ="Cookies", Roles ="StoreOwner")]
    public async Task<IActionResult> Update(string id)
    {
        var product = await _catalogService.GetProductByIdAsync(id);
        var categories = await _catalogService.GetAllCategoriesAsync();


        if (product is not null)
        {
            var productUpdateInput = new ProductUpdateInput
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                DisplayPrice = product.DisplayPrice,
                Image = product.Image

            };
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", product.Id);

            return View(productUpdateInput);
        }

        return RedirectToAction(nameof(Index));
    }



    [Authorize(AuthenticationSchemes ="Cookies", Roles ="StoreOwner")]
    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateInput productUpdateInput)
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        if (ModelState.IsValid is false)
        {
            return View();
        }

        await _catalogService.UpdateProductAsync(productUpdateInput);

        return RedirectToAction(actionName:"", controllerName: "User");
    }

    [Authorize(AuthenticationSchemes ="Cookies", Roles ="StoreOwner")]
    public async Task<IActionResult> Delete(string id)
    {
        await _catalogService.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}