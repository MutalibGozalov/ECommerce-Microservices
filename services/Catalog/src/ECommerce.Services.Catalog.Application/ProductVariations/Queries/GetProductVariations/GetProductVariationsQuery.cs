namespace ECommerce.Services.Catalog.Application.ProductVariations.Queries;
public class GetProductVariationsQuery : IRequest<Response<List<ProductVariationDto>>> { }

public class GetProductVariationsQueryHandler : IRequestHandler<GetProductVariationsQuery, Response<List<ProductVariationDto>>>
{
    private readonly IMongoCollection<ProductVariation> _productVariationCollection;
    private readonly IMapper _mapper;

    public GetProductVariationsQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productVariationCollection = database.GetCollection<ProductVariation>(databaseSettings.ProductVariationCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<ProductVariationDto>>> Handle(GetProductVariationsQuery request, CancellationToken cancellationToken)
    {
        var productVariations = await _productVariationCollection.Find(p => true).ToListAsync();
        return Response<List<ProductVariationDto>>.Success(_mapper.Map<List<ProductVariationDto>>(productVariations), 200);
    }
}
