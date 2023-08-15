using ECommerce.web.Models;
using ECommerce.web.Services.InterfacesL;

namespace ECommerce.web.Services;
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserViewModel> GetUser()
    {
        return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
    }

}