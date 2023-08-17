namespace ECommerce.Web.Services.Interfaces;

public class ProductVariationUpdateInput
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string[] Media { get; set; } = null!;
}
