namespace ECommerce.Services.Catalog.Application.Products.Queries;
public record GetProductByStoreIdQuery(int StoreId) : IRequest<Response<List<ProductDto>>>;

public class GetProductByStoreIdQueryHandler : IRequestHandler<GetProductByStoreIdQuery, Response<List<ProductDto>>>
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public GetProductByStoreIdQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<ProductDto>>> Handle(GetProductByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var products = await _productCollection.Find(p => p.StoreId == request.StoreId).ToListAsync();
        if (products.Any())
        {
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(c => c.Id == product.CategoryId).FirstAsync();
            }
        }
        else products = new List<Product>();

        return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
    }
}
