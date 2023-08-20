namespace ECommerce.Web.Models.Order;
public class OrderCreateInput
{
    public OrderCreateInput()
    {
        OrderItems = new List<OrderItemCreateInput>();
    }
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public List<OrderItemCreateInput> OrderItems { get; set; } = null!;
}
