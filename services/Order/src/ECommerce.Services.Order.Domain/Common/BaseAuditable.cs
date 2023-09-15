namespace ECommerce.Services.Order.Domain.Common;
public class BaseAuditable : BaseEntity
 {
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
 }