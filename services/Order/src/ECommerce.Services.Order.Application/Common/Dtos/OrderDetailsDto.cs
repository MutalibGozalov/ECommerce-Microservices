
namespace ECommerce.Services.Order.Application.Common.Dtos;
public class OrderDetailsDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public byte ProductQuantity { get; set; }
    public TrackingDto? Tracking { get; set; }
    public int TrackingId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}