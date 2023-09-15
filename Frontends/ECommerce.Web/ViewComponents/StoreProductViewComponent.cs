using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.ViewComponents
{
    public class StoreProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}