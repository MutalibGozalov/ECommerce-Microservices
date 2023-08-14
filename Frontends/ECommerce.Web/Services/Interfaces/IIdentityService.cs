using ECommerce.Shared.Dtos;
using IdentityModel.Client;
namespace ECommerce.Web.Services.Interfaces;
public interface IIdentityService
{
    Task<Response<bool>> SignIn();
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}