
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Order.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController  : CustomBaseController
{
    private ISender? _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public OrderController(ISender? mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;

    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await Mediator.Send(new GetOrdersQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetOrderById")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var response = await Mediator.Send(new GetOrderByIdQuery {Id = id});
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetOrdersByUser")]
    public async Task<IActionResult> GetOrdersByUserId( )
    {
        var response = await Mediator.Send(new GetOrdersByUserIdQuery{UserId = _sharedIdentityService.GetUserId});
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
    {
        var response = await Mediator.Send(createOrderCommand);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder(UpdateOrderCommand updateOrderCommand)
    {
        var response = await Mediator.Send(updateOrderCommand);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var response = await Mediator.Send(new DeleteOrderCommand {Id = id});
        return CreateActionResultInstance(response);
    }
}