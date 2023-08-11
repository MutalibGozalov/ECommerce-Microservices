namespace ECommerce.Services.Order.Application.ShippingItems.Handlers;
public class GetShippingByIdQueryHandler : IRequestHandler<GetShippingByIdQuery, Response<ShippingDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetShippingByIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<Response<ShippingDto>> Handle(GetShippingByIdQuery request, CancellationToken cancellationToken)
    {
        var shipping = await _context.Shippings.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        var shippingDto = _mapper.Map<ShippingDto>(shipping);
        if (shipping is not null)
            return Response<ShippingDto>.Success(shippingDto, 200);

        return Response<ShippingDto>.Failure("Shipping not found", 404);
    }

}