namespace ECommerce.Web.Services.Interfaces;

public class CategoryUpdateInput
{
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}
