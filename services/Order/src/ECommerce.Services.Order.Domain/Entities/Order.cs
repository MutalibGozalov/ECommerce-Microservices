namespace ECommerce.Services.Order.Domain.Entities;
public class Order : BaseAuditable
{
    public string UserId { get; set; } = null!;
    public Shipping Shipping { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public List<OrderDetails> OrderDetails { get; set; } = null!;
}