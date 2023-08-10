
namespace ECommerce.Services.Catalog.Application.Products.Commands.CreateProduct;
using ECommerce.Services.Catalog.Application.Products;
public class CreateProductCommand : IRequest<Response<ProductDto>>, IMapFrom<Product>
{
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal DisplayPrice { get; set; }
    public int StoreId { get; set; }
    public string Image { get; set; } = null!;
    public string[] ProductVariations { get; set; } = null!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDto>>
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<Product>(request);
        newProduct.CreatedAt = DateTime.UtcNow;
        newProduct.ModifiedAt = DateTime.UtcNow;
        await _productCollection.InsertOneAsync(newProduct);

        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(newProduct), 200);
    }
}