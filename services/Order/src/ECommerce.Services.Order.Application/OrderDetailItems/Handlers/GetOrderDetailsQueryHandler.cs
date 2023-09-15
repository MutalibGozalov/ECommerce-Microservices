
namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers;
public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, Response<List<OrderDetailDto>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderDetailsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }


    public async Task<Response<List<OrderDetailDto>>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = _mapper.Map<List<OrderDetailDto>>(await _context.OrderDetails.AsNoTracking().ToListAsync(cancellationToken));
        return Response<List<OrderDetailDto>>.Success(orderDetails, 200);
    }
}