namespace ECommerce.Services.Payment.Models;
public class OrderDto
{
    public OrderDto()
    {
        OrderItems = new();
    }
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public List<OrderItemDto>? OrderItems { get; set; }
}