namespace ECommerce.Web.Models.Order;
public class OrderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int[] OrderDetailIds { get; set; } = null!;
    public List<OrderItemViewModel> OrderItem { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}