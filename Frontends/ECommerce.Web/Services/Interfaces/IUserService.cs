using ECommerce.Web.Models;

namespace ECommerce.Web.Services.Interfaces;
public interface IUserService
{
    Task<UserViewModel> GetUser();
    Task<bool> SignUp(SignUpInput signUpInput);
}