namespace ECommerce.Services.Order.Application.TrackingItems.Commands;
public class DeleteTrackingCommand : IRequest<Response<NoContent>>
{
    public int Id { get; set; }
}