namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers;
public class GetOrderDetailsByOrderIdQueryHandler : IRequestHandler<GetOrderDetailsByOrderIdQuery, Response<List<OrderDetailDto>>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetOrderDetailsByOrderIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<List<OrderDetailDto>>> Handle(GetOrderDetailsByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await _context.OrderDetails.AsNoTracking().Where(o => o.OrderId == request.OrderId).ToListAsync(cancellationToken);
        return Response<List<OrderDetailDto>>.Success( _mapper.Map<List<OrderDetailDto>>(orderDetails), 200);
    }

}