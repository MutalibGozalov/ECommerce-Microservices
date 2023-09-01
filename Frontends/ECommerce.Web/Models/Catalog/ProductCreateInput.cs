using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Models.Catalog;
public class ProductCreateInput
{
    public ProductCreateInput()
    {
        ProductVariationIds = new();
    }
    public string CategoryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal DisplayPrice { get; set; }
    public int StoreId { get; set; }
    public string? Image { get; set; } = null!;
    public List<string>? ProductVariationIds { get; set; } = null!;
    public IFormFile? PhotoFormFile { get; set; }
    public List<ProductVariationCreateInput>? ProductVariations { get; set; }
}