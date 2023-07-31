
namespace ECommerce.Services.Catalog.Application.Categories.Commands.CreateCategory;
public class CategoryCreateDto : IMapFrom<Category>
{
    public string Name { get; set; } = null!;
    public string? SubcategoryId { get; set; }
}