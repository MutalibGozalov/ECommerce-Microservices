
namespace ECommerce.Services.Catalog.Application.Categories;
public class CategoryDto  : IMapFrom<Category>
{
    public string Id { get; set; }  = null!;
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}