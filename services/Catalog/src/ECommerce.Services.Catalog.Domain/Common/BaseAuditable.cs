namespace ECommerce.Services.Catalog.Domain.Common;
public class BaseAuditable : BaseEntity
 {
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime ModifiedAt { get; set; }
 }