namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers;
 public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, Response<NoContent>>
 {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOrderDetailCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<Response<NoContent>> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        
        var orderDetail = await _context.OrderDetails.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.Id);
        if (orderDetail is not null)
        {
            orderDetail = _mapper.Map<OrderDetail>(request);
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(200);
        }

        return Response<NoContent>.Failure("Order Detail not found", 404);
    }

}