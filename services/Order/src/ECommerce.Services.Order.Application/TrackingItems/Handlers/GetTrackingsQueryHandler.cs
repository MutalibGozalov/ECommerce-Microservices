namespace ECommerce.Services.Order.Application.TrackingItems.Handlers;
public class GetTrackingsQueryHandler : IRequestHandler<GetTrackingsQuery, Response<List<TrackingDto>>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetTrackingsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<List<TrackingDto>>> Handle(GetTrackingsQuery request, CancellationToken cancellationToken)
    {
        var trackings = await _context.Trackings.AsNoTracking().ToListAsync(cancellationToken);
        var trackingDtos = _mapper.Map<List<TrackingDto>>(trackings);
        return Response<List<TrackingDto>>.Success(trackingDtos, 200);
    }

}