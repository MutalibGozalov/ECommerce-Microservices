namespace ECommerce.Services.Order.Application.OrderDetailItems.Handlers
{
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, Response<NoContent>>
    {
        private readonly IAppDbContext _context;

        public DeleteOrderDetailCommandHandler(IAppDbContext context)
        {
            _context = context;
        }


        public Task<Response<NoContent>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.Id == request.Id);
            if (orderDetail is not null)  
            {
            _context.OrderDetails.Remove(orderDetail);
            return Task.FromResult(Response<NoContent>.Success(204));
            }
            return Task.FromResult(Response<NoContent>.Failure("Order not found", 404));
        }

    }
}