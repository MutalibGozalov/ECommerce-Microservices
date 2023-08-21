namespace ECommerce.Services.Order.Application.TrackingItems.Commands;
public class CreateTrackingCommand : IRequest<Response<int>>
{
    public string TrackingNumber { get; set; } = null!;
    
}