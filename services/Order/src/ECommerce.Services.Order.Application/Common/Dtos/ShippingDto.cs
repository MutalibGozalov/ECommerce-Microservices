
namespace ECommerce.Services.Order.Application.Common.Dtos;
 public class ShippingDto: IMapFrom<Shipping>
 {
    public int Id { get; set; }
    public string ShippingName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
 }