
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Order.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShippingController  : CustomBaseController
{
    private ISender? _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public ShippingController(ISender? mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;

    }

    [HttpGet]
    public async Task<IActionResult> GetShippings()
    {
        var response = await Mediator.Send(new GetShippingsQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetShippingById")]
    public async Task<IActionResult> GetShippingById(int id)
    {
        var response = await Mediator.Send(new GetShippingByIdQuery {Id = id});
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShipping(CreateShippingCommand createShippingCommand)
    {
        var response = await Mediator.Send(createShippingCommand);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateShipping(UpdateShippingCommand updateShippingCommand)
    {
        var response = await Mediator.Send(updateShippingCommand);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteShipping(int id)
    {
        var response = await Mediator.Send(new DeleteShippingCommand {Id = id});
        return CreateActionResultInstance(response);
    }
}