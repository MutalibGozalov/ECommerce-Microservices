using ECommerce.Web.Models.Cart;

namespace ECommerce.Web.Services.Interfaces;
public interface ICartService
{
    Task<bool> SaveOrUpdate(CartViewModel cartViewModel);
    Task<CartViewModel> Get();
    Task<bool> Delete();
    Task AddItemToCart(CartItemViewModel cartItemViewModel);
    Task<bool> RevokeCartItem(string productId);
    Task<bool> DeleteCartItem(string productId);
    Task<bool> ApplyDiscount(string discountCode);
    Task<bool> CancelDiscount();
}