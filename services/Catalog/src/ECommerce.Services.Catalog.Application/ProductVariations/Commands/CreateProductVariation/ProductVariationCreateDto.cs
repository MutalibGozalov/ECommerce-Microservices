
namespace ECommerce.Services.Catalog.Application.ProductVariations.Commands.CreateProductVariation;
public class ProductVariationCreateDto
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string[] Media { get; set; } = null!;
}