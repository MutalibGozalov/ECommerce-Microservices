using ECommerce.web.Models;

namespace ECommerce.web.Services.InterfacesL;
public interface IUserService
{
    Task<UserViewModel> GetUser();
}