namespace ECommerce.Services.Catalog.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<Response<CategoryDto>>, IMapFrom<Category>
{
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryDto>>
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        this._mapper = mapper;
    }
    public async Task<Response<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        category.CreatedAt = DateTime.UtcNow;
        category.ModifiedAt = DateTime.UtcNow;
        await _categoryCollection.InsertOneAsync(category);
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}