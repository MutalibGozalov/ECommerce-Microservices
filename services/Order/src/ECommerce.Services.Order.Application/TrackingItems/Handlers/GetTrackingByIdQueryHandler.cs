namespace ECommerce.Services.Order.Application.TrackingItems.Handlers;
public class GetTrackingByIdQueryHandler : IRequestHandler<GetTrackingByIdQuery, Response<TrackingDto>>
{
    private readonly IAppDbContext _context;

    public GetTrackingByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<Response<TrackingDto>> Handle(GetTrackingByIdQuery request, CancellationToken cancellationToken)
    {
       var tracking = await _context.Trackings.FirstOrDefaultAsync(t => t.Id == request.Id);
       if (tracking is not null)
       {
            var trackingDto = new TrackingDto {Id = request.Id, TrackingNumber = tracking.TrackingNumber};
            return Response<TrackingDto>.Success(trackingDto, 200);
       }

       return Response<TrackingDto>.Failure("Tracking not found", 404);
    }

}