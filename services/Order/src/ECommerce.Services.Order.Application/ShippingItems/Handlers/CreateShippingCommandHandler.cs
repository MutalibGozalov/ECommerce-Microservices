namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class CreateShippingCommandHandler : IRequestHandler<CreateShippingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public CreateShippingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = new Shipping{ShippingName = request.ShippingName};

        await _context.Shippings.AddAsync(shipping, cancellationToken);

        return Response<NoContent>.Success(204);
    }

}