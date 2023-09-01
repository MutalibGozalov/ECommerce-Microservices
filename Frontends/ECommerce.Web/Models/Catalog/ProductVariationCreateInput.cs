namespace ECommerce.Web.Services.Interfaces;

public class ProductVariationCreateInput
{
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public List<string> Media { get; set; } = null!;
    public IFormFileCollection? MediaFormFiles { get; set; }
    

}