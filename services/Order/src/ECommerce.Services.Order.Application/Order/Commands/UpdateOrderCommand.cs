
namespace ECommerce.Services.Order.Application.Order.Commands;
public class UpdateOrderCommand : IRequest<Response<NoContent>>, IMapFrom<OrderModel>
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
}