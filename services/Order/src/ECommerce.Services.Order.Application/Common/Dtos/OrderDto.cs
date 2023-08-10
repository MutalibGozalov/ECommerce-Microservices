
namespace ECommerce.Services.Order.Application.Common.Dtos;
public class OrderDto: IMapFrom<OrderModel>
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public ShippingDto Shipping { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}