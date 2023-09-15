using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
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

    public async Task<bool> SignUp(SignUpInput signUpInput)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/user/signup", signUpInput);

        return response.IsSuccessStatusCode;
    }

}