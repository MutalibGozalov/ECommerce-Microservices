namespace ECommerce.Web.Services.Interfaces;

public class ProductVariationCreateInput
{
    public string Id { get; set; } = null!;
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string[] Media { get; set; } = null!;
}