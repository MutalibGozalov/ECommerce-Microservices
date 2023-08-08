
using ECommerce.Services.Discount.Dtos;
using ECommerce.Services.Discount.Services;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Discount.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DiscountController : CustomBaseController
{
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly IDiscountService _discountService;

    public DiscountController(ISharedIdentityService sharedIdentityService, IDiscountService discountService)
    {
        _sharedIdentityService = sharedIdentityService;
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResultInstance(await _discountService.GetAll());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return CreateActionResultInstance(await _discountService.GetById(id));
    }

    [HttpGet]
    [Route("GetByCode/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var userId = _sharedIdentityService.GetUserId;
        return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, userId));
    }
    [HttpPost]
    public async Task<IActionResult> Create(DiscountDto discountDto)
    {
        var result = await _discountService.Create(discountDto);
        return CreateActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update(DiscountDto discountDto)
    {
        var result = await _discountService.Update(discountDto);
        return CreateActionResultInstance(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _discountService.Delete(id);
        return CreateActionResultInstance(result);
    } 
}