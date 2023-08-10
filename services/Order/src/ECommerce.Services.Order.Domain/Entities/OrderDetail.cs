namespace ECommerce.Services.Order.Domain.Entities;
public class OrderDetail : BaseAuditable
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public byte ProductQuantity { get; set; }
    public Tracking? Tracking { get; set; }
    public int TrackingId { get; set; }
}