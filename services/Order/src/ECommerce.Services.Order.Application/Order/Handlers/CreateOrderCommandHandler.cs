namespace ECommerce.Services.Order.Application.Order.Handlers;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<NoContent>>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateOrderCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<NoContent>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderModel newOrder = _mapper.Map<OrderModel>(request);
        await _context.Orders.AddAsync(newOrder, cancellationToken);
        return Response<NoContent>.Success(204);
    }

}
