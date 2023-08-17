
namespace ECommerce.Services.Catalog.Application.Products.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<Response<NoContent>>, IMapFrom<Product>
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal DisplayPrice { get; set; }
    public int StoreId { get; set; }
    public string? Image { get; set; } = null!;
    public string[]? ProductVariations { get; set; } = null!;
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<NoContent>>
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        _mapper = mapper;
    }
    public async Task<Response<NoContent>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var product = _mapper.Map<Product>(request);
        product.ModifiedAt = DateTime.UtcNow;
        var productOld =  _productCollection.Find(p => p.Id == request.Id).First();
        product.CreatedAt = productOld.CreatedAt;
        var result = await _productCollection.FindOneAndReplaceAsync(p => p.Id == request.Id, product);

        if (result is null)
        {
            return Response<NoContent>.Failure("Product not found", 404);
        }

        return Response<NoContent>.Success(200);
    }
}