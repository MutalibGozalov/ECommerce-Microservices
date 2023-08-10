
using ECommerce.Services.Order.Application.Order.Queries;

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
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
        var orderDto = _mapper.Map<OrderDto>(order);
        return Response<OrderDto>.Success(orderDto, 200);
    }
}