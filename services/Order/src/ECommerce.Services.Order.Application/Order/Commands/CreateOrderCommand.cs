
namespace ECommerce.Services.Order.Application.Order.Commands
{
    public class CreateOrderCommand : IRequest<Response<NoContent>>, IMapFrom<OrderModel>
    {
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    }
}