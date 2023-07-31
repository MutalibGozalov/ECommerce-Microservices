
namespace ECommerce.Services.Catalog.Application.Categories.Commands.UpdateCategory;
public class CategoryUpdateDto
{
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}