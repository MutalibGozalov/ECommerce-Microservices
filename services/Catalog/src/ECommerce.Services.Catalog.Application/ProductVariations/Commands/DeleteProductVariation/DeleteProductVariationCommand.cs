namespace ECommerce.Services.Catalog.Application.ProductVariations.Commands.DeleteProductVariation;
public class DeleteProductVariationCommand : IRequest<Response<NoContent>>
{
   public string  Id { get; set; } = null!;
}

public class DeleteProductVariationCommandHandler : IRequestHandler<DeleteProductVariationCommand, Response<NoContent>>
{
    private readonly IMongoCollection<ProductVariation> _productVariationCollection;
    private readonly IMapper _mapper;

    public DeleteProductVariationCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productVariationCollection = database.GetCollection<ProductVariation>(databaseSettings.ProductVariationCollectionName);

        _mapper = mapper;
    }
    public async Task<Response<NoContent>> Handle(DeleteProductVariationCommand request, CancellationToken cancellationToken)
    {
        var result = await _productVariationCollection.DeleteOneAsync(p => p.Id == request.Id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }
        else return Response<NoContent>.Failure("Product not found", 404);
    }
}
