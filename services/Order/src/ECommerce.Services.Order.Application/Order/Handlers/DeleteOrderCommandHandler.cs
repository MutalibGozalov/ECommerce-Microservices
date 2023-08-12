
namespace ECommerce.Services.Order.Application.Order.Handlers;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public DeleteOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<NoContent>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == request.Id);
        if (order is not null)  
        {
          _context.Orders.Remove(order);
          await _context.SaveChangesAsync(cancellationToken);
          return Response<NoContent>.Success(200);
        }
        return Response<NoContent>.Failure("Order not found", 404);
    }
}