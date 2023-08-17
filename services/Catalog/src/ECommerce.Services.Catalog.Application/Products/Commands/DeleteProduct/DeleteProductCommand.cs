namespace ECommerce.Services.Catalog.Application.Products.Commands.DeleteProduct;
public class DeleteProductCommand : IRequest<Response<NoContent>>
{
    public string Id { get; set; } = null!;
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<NoContent>>
{
    private readonly IMongoCollection<Product> _productCollection;
    public DeleteProductCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
    }
    public async Task<Response<NoContent>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _productCollection.DeleteOneAsync(p => p.Id == request.Id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(200);
        }
        else return Response<NoContent>.Failure("Product not found", 404);
    }
}