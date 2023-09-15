namespace ECommerce.Web.Services.Interfaces;

public class CategoryCreateInput
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; } 
}