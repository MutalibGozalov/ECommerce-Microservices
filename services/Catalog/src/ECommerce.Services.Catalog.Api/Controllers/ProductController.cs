using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.Catalog.Application.Products.Commands.CreateProduct;
using ECommerce.Services.Catalog.Application.Products.Commands.UpdateProduct;
using ECommerce.Services.Catalog.Application.Products.Commands.DeleteProduct;
using ECommerce.Services.Catalog.Application.Products.Queries;
using MassTransit;
using ECommerce.Shared.Messages;
using JetBrains.Annotations;


namespace ECommerce.Services.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : CustomBaseController
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetProductsQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await Mediator.Send(new GetProductByIdQuery(id));
        return CreateActionResultInstance(response);
    }

    [HttpGet("{storeId}")]
    public async Task<IActionResult> GetByStoreId(int storeId)
    {
        var response = await Mediator.Send(new GetProductByStoreIdQuery(storeId));
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
/*         command.ProductVariations.ForEach (async item =>
        {
            var itemResponse = await Mediator.Send(item);
            command.ProductVariationIds.Append(itemResponse.Data.Id);
        }); */
        var response = await Mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductCommand command)
    {
        var response = await Mediator.Send(command);

        if (response.IsSuccessful)
        {
            await _publishEndpoint.Publish(new ProductNameChangedEvent {ProductId = command.Id, ProductName = command.Name});
        }
        
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await Mediator.Send(new DeleteProductCommand {Id = id});
        return CreateActionResultInstance(response);
    }
}