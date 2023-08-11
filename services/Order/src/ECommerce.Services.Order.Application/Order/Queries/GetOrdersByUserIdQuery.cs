namespace ECommerce.Services.Order.Application.Order.Queries;
public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
public string? UserId { get; set; }
}