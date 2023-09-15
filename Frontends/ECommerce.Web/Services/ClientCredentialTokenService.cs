using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Services;
public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _clientAccessTokenCache;
    private readonly HttpClient _httpClient;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _clientSettings = clientSettings.Value;
        _clientAccessTokenCache = clientAccessTokenCache;
        _httpClient = httpClient;

    }


    public async Task<string> GetToken()
    {
        var clientAccessTokenParameters = new ClientAccessTokenParameters() { ForceRenewal = false, Resource = "resource_catalog" };
        var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", clientAccessTokenParameters);

        if (currentToken is not null)
        {
            return currentToken.AccessToken;
        }

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception ?? new Exception("Discovery exception thrown DARLIN!");
        }

        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };

        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

        if (newToken.IsError)
        {
            throw newToken.Exception;
        }

        await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, clientAccessTokenParameters);

        return newToken.AccessToken;
    }

}