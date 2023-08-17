using ECommerce.Web.Models.PhotoStock;

namespace ECommerce.Web.Services.Interfaces;
public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhoto(IFormFile photo);
    Task<bool> DeletePhoto(string photoUrl);

}