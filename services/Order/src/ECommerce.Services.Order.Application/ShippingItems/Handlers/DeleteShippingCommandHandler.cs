namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class DeleteShippingCommandHandler : IRequestHandler<DeleteShippingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _command;

    public DeleteShippingCommandHandler(IAppDbContext command)
    {
        _command = command;
    }


    public async Task<Response<NoContent>> Handle(DeleteShippingCommand request, CancellationToken cancellationToken)
    {
        var deleteShipping = await _command.Shippings.FirstOrDefaultAsync(s => s.Id == request.Id);
        if(deleteShipping is not null)
            return Response<NoContent>.Success(204);
        return Response<NoContent>.Failure("Shipping not found", 404);
    }
}