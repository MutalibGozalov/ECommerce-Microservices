namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class DeleteShippingCommandHandler : IRequestHandler<DeleteShippingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public DeleteShippingCommandHandler(IAppDbContext command)
    {
        _context = command;
    }


    public async Task<Response<NoContent>> Handle(DeleteShippingCommand request, CancellationToken cancellationToken)
    {
        var deleteShipping = await _context.Shippings.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (deleteShipping is not null)
        {
            _context.Shippings.Remove(deleteShipping);
            await _context.SaveChangesAsync(cancellationToken);
            return Response<NoContent>.Success(200);
        }
        return Response<NoContent>.Failure("Shipping not found", 404);
    }
}