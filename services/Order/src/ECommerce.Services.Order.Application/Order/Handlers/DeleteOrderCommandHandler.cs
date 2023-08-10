
namespace ECommerce.Services.Order.Application.Order.Handlers;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public DeleteOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<Response<NoContent>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == request.Id);
        if (order is not null)  
        {
          _context.Orders.Remove(order);
          return Task.FromResult(Response<NoContent>.Success(204));
        }
        return Task.FromResult(Response<NoContent>.Failure("Order not found", 404));
    }
}