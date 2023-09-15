namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class GetShippingsQueryHandler : IRequestHandler<GetShippingsQuery, Response<List<ShippingDto>>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetShippingsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<List<ShippingDto>>> Handle(GetShippingsQuery request, CancellationToken cancellationToken)
    {
        var shippings = await _context.Shippings.AsNoTracking().ToListAsync(cancellationToken);

        return Response<List<ShippingDto>>.Success(_mapper.Map<List<ShippingDto>>(shippings), 200);
    }

}