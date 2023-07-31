namespace ECommerce.Services.Catalog.Application.ProductVariations.Commands.CreateProductVariation;
public class CreateProductVariationCommand : IRequest<Response<ProductVariationDto>>, IMapFrom<ProductVariation>
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string[] Media { get; set; } = null!;
}

public class CreateProductVariationCommandHandler : IRequestHandler<CreateProductVariationCommand, Response<ProductVariationDto>>
{
    private readonly IMongoCollection<ProductVariation> _productVariationCollection;
    private readonly IMapper _mapper;

    public CreateProductVariationCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productVariationCollection = database.GetCollection<ProductVariation>(databaseSettings.ProductVariationCollectionName);

        _mapper = mapper;
    }
    public async Task<Response<ProductVariationDto>> Handle(CreateProductVariationCommand request, CancellationToken cancellationToken)
    {
        var newProductVariation = _mapper.Map<ProductVariation>(request);
        newProductVariation.CreatedAt = DateTime.UtcNow;
        newProductVariation.ModifiedAt = DateTime.UtcNow;
        await _productVariationCollection.InsertOneAsync(newProductVariation);

        return Response<ProductVariationDto>.Success(_mapper.Map<ProductVariationDto>(newProductVariation), 200);
    }
}
