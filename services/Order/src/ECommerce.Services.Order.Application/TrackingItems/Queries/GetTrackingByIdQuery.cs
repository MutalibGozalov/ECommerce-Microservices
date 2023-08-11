namespace ECommerce.Services.Order.Application.TrackingItems.Queries;
public class GetTrackingByIdQuery : IRequest<Response<TrackingDto>>
{
    public int Id { get; set; }
}