namespace ECommerce.Services.Catalog.Domain.Entities;
public class Category : BaseAuditable
{   
    public string Name { get; set; } = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    public string? SubcategoryId { get; set; }
}