using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.Catalog.Application.ProductVariations.Commands.CreateProductVariation;
using ECommerce.Services.Catalog.Application.ProductVariations.Commands.UpdateProductVariation;
using ECommerce.Services.Catalog.Application.ProductVariations.Commands.DeleteProductVariation;
using ECommerce.Services.Catalog.Application.ProductVariations.Queries;

namespace ECommerce.Services.Catalog.Api.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class ProductVariationController : CustomBaseController
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public ProductVariationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetProductVariationsQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await Mediator.Send(new GetProductVariationByIdQuery(id));
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductVariationCommand command)
    {
        var response = await Mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductVariationCommand command)
    {
        var response = await Mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteProductVariationCommand command)
    {
        var response = await Mediator.Send(command);
        return CreateActionResultInstance(response);
    }
}