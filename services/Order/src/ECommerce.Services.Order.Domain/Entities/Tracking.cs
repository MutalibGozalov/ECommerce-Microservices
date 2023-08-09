namespace ECommerce.Services.Order.Domain.Entities;
public class Tracking : BaseAuditable
{
    public string TrackingNumber { get; set; } = null!;
    public OrderDetails OrderProduct { get; set; }  = null!;
}