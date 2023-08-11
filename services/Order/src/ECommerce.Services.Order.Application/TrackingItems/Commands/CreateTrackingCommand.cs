namespace ECommerce.Services.Order.Application.TrackingItems.Commands;
public class CreateTrackingCommand : IRequest<Response<NoContent>>
{
    public string TrackingNumber { get; set; } = null!;
    
}