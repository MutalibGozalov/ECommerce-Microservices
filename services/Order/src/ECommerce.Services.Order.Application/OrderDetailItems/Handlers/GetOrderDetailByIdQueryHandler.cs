namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers;
public class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, Response<OrderDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetOrderDetailByIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<OrderDetailDto>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.OrderDetails.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.Id);
        var orderDto = _mapper.Map<OrderDetailDto>(order);
        if (orderDto is not null)
            return Response<OrderDetailDto>.Success(orderDto, 200);
        
        return Response<OrderDetailDto>.Failure("Order not found", 404);

    }
}