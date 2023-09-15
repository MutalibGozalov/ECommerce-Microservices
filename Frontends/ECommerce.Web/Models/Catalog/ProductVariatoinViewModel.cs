namespace ECommerce.Web.Models.Catalog;
public class ProductVariatoinViewModel
{
    public string Id { get; set; } = null!;
    public int SKU { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string[] Media { get; set; } = null!;
    public ProductViewModel? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}