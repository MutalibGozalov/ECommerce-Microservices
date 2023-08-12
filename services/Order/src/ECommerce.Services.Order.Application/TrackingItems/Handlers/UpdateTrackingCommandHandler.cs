namespace ECommerce.Services.Order.Application.TrackingItems.Handlers;
public class UpdateTrackingCommandHandler : IRequestHandler<UpdateTrackingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public UpdateTrackingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(UpdateTrackingCommand request, CancellationToken cancellationToken)
    {
        var updateTracking = new Tracking { Id = request.Id, TrackingNumber = request.TrackingNumber };
        var tracking = await _context.Trackings.FirstOrDefaultAsync(t => t.Id == request.Id);

        if (tracking is not null)
        {
            _context.Trackings.Update(updateTracking);
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Failure("Tracking not found", 404);
    }

}