namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class UpdateShippingCommandHandler : IRequestHandler<UpdateShippingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public UpdateShippingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(UpdateShippingCommand request, CancellationToken cancellationToken)
    {
        var updateShipping = new Shipping {Id = request.Id, ShippingName = request.ShippingName};
        var shipping = await _context.Shippings.FirstOrDefaultAsync(s => s.Id == request.Id);

        if (shipping is not null)
        {
            _context.Shippings.Update(updateShipping);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(200);
        }

        return Response<NoContent>.Failure("Shipping not found", 404);
    }

}