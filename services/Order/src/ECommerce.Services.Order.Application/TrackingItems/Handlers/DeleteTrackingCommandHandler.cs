namespace ECommerce.Services.Order.Application.TrackingItems.Handlers;
public class DeleteTrackingCommandHandler : IRequestHandler<DeleteTrackingCommand, Response<NoContent>>
{
    private readonly IAppDbContext _context;

    public DeleteTrackingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<NoContent>> Handle(DeleteTrackingCommand request, CancellationToken cancellationToken)
    {

        var deleteTracking = await _context.Trackings.FirstOrDefaultAsync(t => t.Id == request.Id);

        if (deleteTracking is not null)
        {
            _context.Trackings.Remove(deleteTracking);
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Failure("Tracking not found", 404);
    }

}