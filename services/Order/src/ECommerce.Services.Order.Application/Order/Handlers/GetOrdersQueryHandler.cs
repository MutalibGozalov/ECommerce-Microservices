
using ECommerce.Services.Order.Application.Order.Queries;

namespace ECommerce.Services.Order.Application.Order.Handlers;
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Response<List<OrderDto>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;


    public GetOrdersQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        foreach(var o in orderDtos) {
            var details =  _context.OrderDetails.AsNoTracking().Where(od => od.OrderId == o.Id);
            var idArray = await details.Select(od => od.Id).ToArrayAsync();
            o.OrderDetailIds = idArray;
        };
        return Response<List<OrderDto>>.Success(orderDtos, 200);

    }
}