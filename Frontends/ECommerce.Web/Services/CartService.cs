using ECommerce.Shared.Dtos;
using ECommerce.Web.Models.Cart;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class CartService : ICartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CartViewModel> Get()
    {
        var response = await _httpClient.GetAsync("cart/GetCart");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }
        var cartViewModel = await response.Content.ReadFromJsonAsync<Response<CartViewModel>>();

        return cartViewModel.Data;
    }

    public async Task<bool> SaveOrUpdate(CartViewModel cartViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("cart/SaveOrUpdateCart", cartViewModel);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("cart/DeleteCart");

        return result.IsSuccessStatusCode;
    }

    public async Task AddCartItem(CartItemViewModel cartItemViewModel)
    {
        var cart = await Get();

        if (cart is not null)
            if (cart.CartItems.Any(i => i.ProductId == cartItemViewModel.ProductId) is false)
                cart.CartItems.Add(cartItemViewModel);

        else
        {
            cart = new CartViewModel { };
            cart.CartItems.Add(cartItemViewModel);
        }

        await SaveOrUpdate(cart);
    }

    public async Task<bool> RevokeCartItem(string productId)
    {
        var cart = await Get();

        if (cart is null)
            return false;

        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (cartItem is null)
        {
            return false;
        }

        var result = cart.CartItems.Remove(cartItem);

        if (result is false)
        {
            return false;
        }

        if (cart.CartItems.Any() is false)
        {
            cart.DiscountCode = null;
        }

        return await SaveOrUpdate(cart);

    }

    public Task<bool> ApplyDiscount(string discountCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelDiscount()
    {
        throw new NotImplementedException();
    }




}