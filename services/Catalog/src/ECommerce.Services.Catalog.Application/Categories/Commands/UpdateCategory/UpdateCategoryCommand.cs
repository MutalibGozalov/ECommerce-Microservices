namespace ECommerce.Services.Catalog.Application.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest<Response<NoContent>>, IMapFrom<Category>
{
    public string Id { get; set; }  = null!;
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<NoContent>>
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        this._mapper = mapper;
    }
    public async Task<Response<NoContent>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        category.ModifiedAt = DateTime.UtcNow;
        var result = await _categoryCollection.FindOneAndReplaceAsync(c=>c.Id == request.Id, category);

        if(result is null)
        {
            return Response<NoContent>.Failure("Product not found", 404);
        }

        return Response<NoContent>.Success(200);
    }
}
