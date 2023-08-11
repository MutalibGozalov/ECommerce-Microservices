namespace ECommerce.Services.Order.Application.ShippingItems.Queries
{
    public class GetShippingByIdQuery : IRequest<Response<ShippingDto>>
    {
        public int Id { get; set; }
    }
}