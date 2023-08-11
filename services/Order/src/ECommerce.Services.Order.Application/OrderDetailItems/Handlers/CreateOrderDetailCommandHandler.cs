
namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers;
public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, Response<NoContent>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateOrderDetailCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
       var orderDetail = _mapper.Map<OrderDetail>(request);
       await _context.OrderDetails.AddAsync(orderDetail);
       return Response<NoContent>.Success(204);
    }
}