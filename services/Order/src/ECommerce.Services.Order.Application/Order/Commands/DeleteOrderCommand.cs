
namespace ECommerce.Services.Order.Application.Order.Commands;
public class DeleteOrderCommand : IRequest<Response<NoContent>>
{
    public int Id { get; set; }
}