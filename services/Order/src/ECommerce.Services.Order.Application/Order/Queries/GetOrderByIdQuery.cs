
namespace ECommerce.Services.Order.Application.Order.Queries;
 public class GetOrderByIdQuery : IRequest<Response<OrderDto>>
 {
     public int Id { get; set; }
 }