namespace ECommerce.Web.Models.Catalog;
public class CategoryViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}
