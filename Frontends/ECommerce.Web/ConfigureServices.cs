using ECommerce.Web.Services;
using ECommerce.Web.Services.Interfaces;
using ECommerce.Web.Handler;
using ECommerce.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using ECommerce.Shared.Services;
using ECommerce.Web.Helpers;

namespace Microsoft.Extensions.DependencyInjection;
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
        services.AddHttpClient<IIdentityService, IdentityService>(options =>
        {
            options.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
        });

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

        services.AddHttpClient<IDiscountService, DiscountService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IPaymentService, PaymentService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IOrderService, OrderService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "oidc";
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Auth/SignIn";
            options.ExpireTimeSpan = TimeSpan.FromDays(60);
            options.SlidingExpiration = true;
            options.Cookie.Name = "ecommercewebcookie";
        }).AddOpenIdConnect("oidc", options =>
        {
            options.Authority = "http://localhost:5001";
            options.RequireHttpsMetadata = false;

            options.ClientId = "mvc";
            options.ClientSecret = "secret";
            options.ResponseType = "code";

            options.SaveTokens = true;
        });

        services.AddControllersWithViews();
        return services;
    }
}