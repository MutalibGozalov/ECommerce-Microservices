namespace ECommerce.Services.Order.Domain.Entities;
public class Shipping : BaseAuditable
{
    public string ShippingName { get; set; } = null!;
    public List<Order> Orders { get; set; } = null!;
}