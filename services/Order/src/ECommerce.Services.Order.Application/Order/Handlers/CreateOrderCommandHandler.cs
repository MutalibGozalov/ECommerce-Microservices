namespace ECommerce.Services.Order.Application.Order.Handlers;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<OrderDto>>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateOrderCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderModel newOrder = _mapper.Map<OrderModel>(request);
        await _context.Orders.AddAsync(newOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Response<OrderDto>.Success(_mapper.Map<OrderDto>(newOrder), 200);
    }

}
