namespace ECommerce.Services.Cart.Application.Common.Interfaces;
public interface ICartService
{
    Task<Response<CartDto>> GetCart(string userId);
    Task<Response<bool>> SaveOrUpdate(CartDto cartDto);
    Task<Response<bool>> Delete(string userId);
}