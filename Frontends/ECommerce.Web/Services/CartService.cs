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

    public async Task AddItemToCart(CartItemViewModel cartItemViewModel)
    {
        var cart = await Get();

        if (cart is not null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == cartItemViewModel.ProductId);
            if (cartItem is null)
                cart.CartItems.Add(cartItemViewModel);
            else
                cartItem.Quantity++;
        }
        else
        {
            cart = new CartViewModel();
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
            return false;

        else if (cartItem.Quantity == 1)
        {
            cart.CartItems.Remove(cartItem);
            if (cart.CartItems.Any() is false)
            {
                cart.DiscountCode = null;
            }
            return await SaveOrUpdate(cart);
        }

        cartItem.Quantity--;

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