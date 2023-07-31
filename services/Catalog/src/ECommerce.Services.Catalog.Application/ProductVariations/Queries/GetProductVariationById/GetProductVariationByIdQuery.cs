namespace ECommerce.Services.Catalog.Application.ProductVariations.Queries;
public record GetProductVariationByIdQuery(string Id) : IRequest<Response<ProductVariationDto>>;


public class GetProductVariationByIdQueryHandler : IRequestHandler<GetProductVariationByIdQuery, Response<ProductVariationDto>>
{
    private readonly IMongoCollection<ProductVariation> _productVariationCollection;
    private readonly IMapper _mapper;

    public GetProductVariationByIdQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productVariationCollection = database.GetCollection<ProductVariation>(databaseSettings.ProductVariationCollectionName);

        _mapper = mapper;
    }
    public async Task<Response<ProductVariationDto>> Handle(GetProductVariationByIdQuery request, CancellationToken cancellationToken)
    {
        var productVariation = await _productVariationCollection.Find(p => p.Id == request.Id).FirstAsync();
        if (productVariation is null)
        {
            return Response<ProductVariationDto>.Failure("Product Variation couldn't found", 404);
        }
        return Response<ProductVariationDto>.Success(_mapper.Map<ProductVariationDto>(productVariation), 200);
    }
}
