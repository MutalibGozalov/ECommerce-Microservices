namespace ECommerce.Services.Catalog.Domain.Common;
public class BaseEntity
 {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
     public string Id { get; set; } = null!;
 }
