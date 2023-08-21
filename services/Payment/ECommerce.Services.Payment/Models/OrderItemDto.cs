namespace ECommerce.Services.Payment.Models;
public class OrderItemDto
{
    public int OrderId { get; set; }
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public byte ProductQuantity { get; set; }
    public int TrackingId { get; set; }
}