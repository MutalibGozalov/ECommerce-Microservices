using ECommerce.Shared.Dtos;
using ECommerce.Web.Models.PhotoStock;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class PhotoStockService : IPhotoStockService
{
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
    {
        if (photo is null || photo.Length <= 0)
        {
            return null;
        }

        var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

        using var memoryStream = new MemoryStream();
        
        await photo.CopyToAsync(memoryStream);

        var multiPartContent = new MultipartFormDataContent
        {
            { new ByteArrayContent(memoryStream.ToArray()), "photo", randomFileName }
        };

        var response = await _httpClient.PostAsync("photos", multiPartContent);

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var successResponse =  await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();
        return successResponse.Data;
        
    }
    public async Task<bool> DeletePhoto(string photoUrl)
    {
        var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
        return response.IsSuccessStatusCode;
    }

}