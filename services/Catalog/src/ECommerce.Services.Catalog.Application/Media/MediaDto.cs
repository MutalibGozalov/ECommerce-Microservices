namespace ECommerce.Services.Catalog.Application.Media;
public class MediaDto
{
    public string Id { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}