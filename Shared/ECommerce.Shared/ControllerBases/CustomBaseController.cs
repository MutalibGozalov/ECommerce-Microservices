using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)// where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode

            };
        }
    }
}