namespace ECommerce.Services.Order.Application.OrderDetailItems.Queries;
public class GetOrderDetailsByOrderIdQuery  : IRequest<Response<List<OrderDetailDto>>> 
{
    public int OrderId { get; set; }
}