namespace ECommerce.Services.Catalog.Application.Categories.Queries;
public record GetCategoryByIdQuery(string Id) : IRequest<Response<CategoryDto>>;


public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryDto>>
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        this._mapper = mapper;
    }
    public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
         var category = await _categoryCollection.Find(category => category.Id == request.Id).FirstOrDefaultAsync();
        if (category is null)
        {
            return Response<CategoryDto>.Failure("Category not found", 404);
        }
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}