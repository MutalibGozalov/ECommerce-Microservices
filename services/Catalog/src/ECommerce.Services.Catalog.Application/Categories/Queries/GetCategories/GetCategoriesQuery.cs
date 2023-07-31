namespace ECommerce.Services.Catalog.Application.Categories.Queries;
public class GetCategoriesQuery : IRequest<Response<List<CategoryDto>>> { }

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<List<CategoryDto>>>
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        this._mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();
        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }
}