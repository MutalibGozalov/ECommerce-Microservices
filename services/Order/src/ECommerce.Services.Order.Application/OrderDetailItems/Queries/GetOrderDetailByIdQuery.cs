namespace ECommerce.Services.Order.Application.OrderDetailItems.Queries;
public class GetOrderDetailByIdQuery : IRequest<Response<OrderDetailDto>>
{
    public int Id { get; set; }
}
