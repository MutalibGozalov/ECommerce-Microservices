namespace ECommerce.Services.Catalog.Domain.Entities;
public class ProductVariation : BaseAuditable
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public string[] Media { get; set; }  = null!; 
    // [BsonIgnore]
    // public Product? Product { get; set; }
}
