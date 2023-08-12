namespace ECommerce.Services.Order.Application.TrackingItems.Handlers;
public class CreateTrackingCommandHandler : IRequestHandler<CreateTrackingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public CreateTrackingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(CreateTrackingCommand request, CancellationToken cancellationToken)
    {
        var createTracking = new Tracking {TrackingNumber = request.TrackingNumber};
        await _context.Trackings.AddAsync(createTracking, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<NoContent>.Success(200);
    }
}