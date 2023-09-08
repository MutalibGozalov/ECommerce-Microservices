using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.IdentityServer.Services
{
    public class GoogleIdTokenValidationService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<ApplicationUser> _userManager;
        readonly TokenHandler _tokenHandler;
        public GoogleIdTokenValidationService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            TokenHandler tokenHandler)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }
    
        public async Task<Token> ValidateIdTokenAsync(GoogleLoginVM model)
        {
            ValidationSettings? settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                    { _configuration["ExternalLogin:Google-Client-Id"] }
            };
            Payload payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
    
            UserLoginInfo userLoginInfo = new(model.Provider, payload.Subject, model.Provider);
            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new() { Id = Guid.NewGuid().ToString(), Email = payload.Email, UserName = payload.Email, Provider = model.Provider };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }
    
            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new Exception("Invalid external authentication.");
    
            Token token = _tokenHandler.CreateAccessToken(5);
            return token;
        }
    }
}