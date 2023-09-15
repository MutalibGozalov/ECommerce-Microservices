
namespace ECommerce.Services.Order.Application.Order.Handlers;
public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<NoContent>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public UpdateOrderCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<NoContent>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _context.Orders.AsNoTracking().FirstOrDefault(o => o.Id == request.Id);
        if (order is not null)
        {
            _context.Orders.Update(_mapper.Map<OrderModel>(request));
            await _context.SaveChangesAsync(cancellationToken);
            return Response<NoContent>.Success(200);
        }
        return Response<NoContent>.Failure("Order not found", 404);
    }
}