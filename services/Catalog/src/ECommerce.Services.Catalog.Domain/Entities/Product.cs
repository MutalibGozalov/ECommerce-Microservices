namespace ECommerce.Services.Catalog.Domain.Entities;
public class Product : BaseAuditable
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;    
    public string? Description { get; set; }  
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal DisplayPrice { get; set; } 
    public int StoreId { get; set; }
    public string Image { get; set; } = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    public string[] ProductVariations { get; set; } = null!; 
    [BsonIgnore]
    public Category? Category { get; set; }
}
