using ECommerce.web.Models;
using ECommerce.web.Services.InterfacesL;

namespace ECommerce.web.Services;
public class UserService : IUserService
{
    private readonly IUserService _userService;
    public Task<UserViewModel> GetUser()
    {
        throw new NotImplementedException();
    }

}