namespace ECommerce.Services.Catalog.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest<Response<NoContent>> { public string Id { get; set; } = null!; }

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<NoContent>>
{
    private readonly IMongoCollection<Category> _categoryCollection;


    public DeleteCategoryCommandHandler(IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }
    public async Task<Response<NoContent>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
       var result = await _categoryCollection.DeleteOneAsync(c => c.Id == request.Id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }
        else return Response<NoContent>.Failure("Product not found", 404);
    }
}