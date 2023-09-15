namespace ECommerce.Web.Models.Order;

public class OrderItemCreateInput
{
    public int OrderId { get; set; }
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public byte ProductQuantity { get; set; }
    public int TrackingId { get; set; }
}