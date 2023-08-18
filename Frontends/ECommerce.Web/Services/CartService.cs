using ECommerce.Web.Models.Cart;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class CartService : ICartService
{

    public Task<CartViewModel> Get()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveOrUpdate(CartViewModel cartViewModel)
    {
        throw new NotImplementedException();
    }

    public Task AddCartItem(CartItemViewModel cartItemViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RevokeCartItem(string productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ApplyDiscount(string discountCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelDiscount()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete()
    {
        throw new NotImplementedException();
    }



}