namespace ECommerce.Services.Order.Application.TrackingItems.Commands;
public class UpdateTrackingCommand : IRequest<Response<NoContent>>
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = null!;
}