
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Order.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderDetailController : CustomBaseController
{
    private ISender? _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public OrderDetailController(ISender? mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;

    }

    [HttpGet]
    public async Task<IActionResult> GetOrderDetails()
    {
        var response = await Mediator.Send(new GetOrderDetailsQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetOrderDetailById")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var response = await Mediator.Send(new GetOrderDetailByIdQuery { Id = id });
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand createOrderDetailCommand)
    {
        var response = await Mediator.Send(createOrderDetailCommand);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand updateOrderDetailCommand)
    {
        var response = await Mediator.Send(updateOrderDetailCommand);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var response = await Mediator.Send(new DeleteOrderDetailCommand { Id = id });
        return CreateActionResultInstance(response);
    }
}