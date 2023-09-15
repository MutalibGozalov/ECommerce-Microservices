namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers
{
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, Response<NoContent>>
    {
        private readonly IAppDbContext _context;

        public DeleteOrderDetailCommandHandler(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<Response<NoContent>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.Id == request.Id);
            if (orderDetail is not null)  
            {
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync(cancellationToken);
            return Response<NoContent>.Success(200);
            }
            return Response<NoContent>.Failure("Order not found", 404);
        }

    }
}