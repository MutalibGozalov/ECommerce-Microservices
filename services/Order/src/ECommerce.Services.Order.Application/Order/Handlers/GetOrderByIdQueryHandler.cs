
namespace ECommerce.Services.Order.Application.Order.Handlers;
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;


    public GetOrderByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
       
        if (order is not null)
        {
            var orderDto = _mapper.Map<OrderDto>(order);

            var details = _context.OrderDetails.AsNoTracking().Where(od => od.OrderId == order.Id);
            var idArray = await details.Select(od => od.Id).ToArrayAsync();
            orderDto.OrderDetailIds = idArray;
            return Response<OrderDto>.Success(orderDto, 200);
        }
        return Response<OrderDto>.Failure("Order not found", 404);
    }
}