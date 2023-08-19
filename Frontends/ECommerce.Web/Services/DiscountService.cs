using ECommerce.Shared.Dtos;
using ECommerce.Web.Models.Discount;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;

    public DiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DiscountViewModel> GetDiscount(string discounCode)
    {
        var response = await _httpClient.GetAsync($"discount/GetByCode/{discounCode}");
        if (response.IsSuccessStatusCode is false)
        {
            return null;    
        }

        var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

        return discount.Data;
    }
}