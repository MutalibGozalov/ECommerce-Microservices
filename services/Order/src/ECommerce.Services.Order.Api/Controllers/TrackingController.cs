
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Order.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TrackingController  : CustomBaseController
{
    private ISender? _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public TrackingController(ISender? mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;

    }

    [HttpGet]
    public async Task<IActionResult> GetTrackings()
    {
        var response = await Mediator.Send(new GetTrackingsQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetTrackingById")]
    public async Task<IActionResult> GetTrackingById(int id)
    {
        var response = await Mediator.Send(new GetTrackingByIdQuery {Id = id});
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTracking(CreateTrackingCommand createTrackingCommand)
    {
        var response = await Mediator.Send(createTrackingCommand);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTracking(UpdateTrackingCommand updateTrackingCommand)
    {
        var response = await Mediator.Send(updateTrackingCommand);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTracking(int id)
    {
        var response = await Mediator.Send(new DeleteTrackingCommand {Id = id});
        return CreateActionResultInstance(response);
    }
}