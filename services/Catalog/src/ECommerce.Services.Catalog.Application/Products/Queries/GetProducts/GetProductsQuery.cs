namespace ECommerce.Services.Catalog.Application.Products.Queries;
public class GetProductsQuery : IRequest<Response<List<ProductDto>>> { }

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<List<ProductDto>>>
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }
    public async Task<Response<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productCollection.Find(p => true).ToListAsync(cancellationToken);

        if (products.Any())
        {
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(c => c.Id == product.CategoryId).FirstAsync(cancellationToken);
            }
        }
        else products = new List<Product>();

        var productDtos = _mapper.Map<List<ProductDto>>(products);

        foreach (var productDto in productDtos)
        {
            productDto.CategoryName = _mapper.Map<Product>(productDto).Category.Name;
        }
        return Response<List<ProductDto>>.Success(productDtos, 200);
    }
}