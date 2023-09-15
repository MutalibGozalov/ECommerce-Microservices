using ECommerce.Web.Models.Discount;

namespace ECommerce.Web.Services.Interfaces;
public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string discounCode);
}