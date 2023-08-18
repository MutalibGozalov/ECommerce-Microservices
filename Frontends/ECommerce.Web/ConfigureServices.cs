using ECommerce.Web.Services;
using ECommerce.Web.Services.Interfaces;
using ECommerce.Web.Handler;
using ECommerce.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using ECommerce.Shared.Services;
using ECommerce.Web.Helpers;

namespace  Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddMvcWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

        services.AddHttpContextAccessor();

        services.AddAccessTokenManagement();

        services.AddSingleton<PhotoHelper>();

        services.AddScoped<ISharedIdentityService, SharedIdentityService>();

        var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();


        services.AddScoped<ResourceOwnerPasswordTokenHandler>();
        services.AddScoped<ClientCredentialTokenHandler>();

        services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
        services.AddHttpClient<IIdentityService, IdentityService>();

        services.AddHttpClient<ICatalogService, CatalogService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IPhotoStockService, PhotoStockService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IUserService, UserService>(options =>
        {
            options.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<ICartService, CartService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Cart.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Auth/SignIn";
            options.ExpireTimeSpan = TimeSpan.FromDays(60);
            options.SlidingExpiration = true;
            options.Cookie.Name = "ecommercewebcookie";
        });

        services.AddControllersWithViews();
        return services;
    }
}