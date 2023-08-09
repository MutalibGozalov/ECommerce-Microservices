
namespace ECommerce.Services.Order.Application.Common.Dtos;
public class TrackingDto
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}