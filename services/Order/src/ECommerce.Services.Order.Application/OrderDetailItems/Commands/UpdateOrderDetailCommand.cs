namespace ECommerce.Services.Order.Application.OrderDetailItems.Commands;

public class UpdateOrderDetailCommand : IRequest<Response<NoContent>>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public byte ProductQuantity { get; set; }
    public int TrackingId { get; set; }
}
