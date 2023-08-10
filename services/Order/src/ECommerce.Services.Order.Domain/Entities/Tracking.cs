namespace ECommerce.Services.Order.Domain.Entities;
public class Tracking : BaseAuditable
{
    public string TrackingNumber { get; set; } = null!;
    public OrderDetail OrderProduct { get; set; }  = null!;
}