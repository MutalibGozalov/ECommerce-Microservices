namespace ECommerce.Services.Order.Application.Order.Handlers;

    public class CreateOrderCommandValidationHandler : IRequestHandler<CreateOrder, Response<NoContent>>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateOrderCommandValidationHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<NoContent>> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        OrderModel newOrder = _mapper.Map<OrderModel>(request);
        await _context.Orders.AddAsync(newOrder, cancellationToken);
        return Response<NoContent>.Success(204);
    }

}
