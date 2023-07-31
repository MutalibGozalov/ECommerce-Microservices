
namespace ECommerce.Services.Catalog.Application.ProductVariations.Commands.UpdateProductVariation;
public class ProductVariationUpdateDto
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string[] Media { get; set; } = null!;
}