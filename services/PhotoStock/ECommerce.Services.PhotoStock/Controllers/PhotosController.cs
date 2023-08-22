using ECommerce.Services.PhotoStock.Dtos;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.PhotoStock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo is not null && photo.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
                await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = photo.FileName; 

            PhotoDto photoDto = new() {Url = returnPath};

            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(Response<PhotoDto>.Failure("photo is empty", 400));
    }

    [HttpDelete]
    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
        if (System.IO.File.Exists(path) is false)
        {
           return CreateActionResultInstance(Response<NoContent>.Failure("photo not found", 404));
        }
        System.IO.File.Delete(path);

        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}