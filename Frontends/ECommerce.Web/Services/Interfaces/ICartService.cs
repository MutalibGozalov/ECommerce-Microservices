using ECommerce.Web.Models.Cart;

namespace ECommerce.Web.Services.Interfaces;
public interface ICartService
{
    Task<bool> SaveOrUpdate(CartViewModel cartViewModel);
    Task<CartViewModel> Get();
    Task<bool> Delete();
    Task AddCartItem(CartItemViewModel cartItemViewModel);
    Task<bool> RevokeCartItem(string productId);
    Task<bool> ApplyDiscount(string discountCode);
    Task<bool> CancelDiscount();
}