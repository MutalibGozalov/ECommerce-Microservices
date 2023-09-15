namespace ECommerce.Services.Order.Application.OrderDetailItems.Commands;
public class DeleteOrderDetailCommand : IRequest<Response<NoContent>>
{
public int Id { get; set; }
}