using ECommerce.Shared.Dtos;
using ECommerce.Web.Models;
using IdentityModel.Client;
namespace ECommerce.Web.Services.Interfaces;
public interface IIdentityService
{
    Task<Response<bool>> SignIn(SigninInput signinInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}