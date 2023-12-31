
namespace ECommerce.Services.Order.Application.Order.Handlers;
public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetOrdersByUserIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userOrders = await _context.Orders.Where(o => o.UserId == request.UserId).AsNoTracking().ToListAsync();
        var orderDtos = _mapper.Map<List<OrderDto>>(userOrders);
        
         foreach(var o in orderDtos) {
            var details =  _context.OrderDetails.AsNoTracking().Where(od => od.OrderId == o.Id);
            var idArray = await details.Select(od => od.Id).ToArrayAsync();
            o.OrderDetailIds = idArray;
        };
        return Response<List<OrderDto>>.Success(orderDtos, 200);
    }

}