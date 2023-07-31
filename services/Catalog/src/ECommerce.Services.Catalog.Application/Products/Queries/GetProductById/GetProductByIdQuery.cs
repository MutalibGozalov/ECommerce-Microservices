namespace ECommerce.Services.Catalog.Application.Products.Queries;
public record GetProductByIdQuery(string Id) : IRequest<Response<ProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDto>>
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = _productCollection.Find(p => p.Id == request.Id).FirstOrDefault();
        if (product is null)
        {
            return Response<ProductDto>.Failure("Product couldn't found", 404);
        }
        product.Category = await _categoryCollection.Find(c => c.Id == product.CategoryId).FirstAsync();
        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
    }
}