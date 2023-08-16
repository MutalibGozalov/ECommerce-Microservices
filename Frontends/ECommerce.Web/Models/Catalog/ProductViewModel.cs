namespace ECommerce.Web.Models.Catalog;
public class ProductViewModel
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal DisplayPrice { get; set; }
    public int StoreId { get; set; }
    public string Image { get; set; } = null!;
    public string[] ProductVariations { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}