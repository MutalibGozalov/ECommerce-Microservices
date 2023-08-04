using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.IdentityServer.Dtos;
using ECommerce.IdentityServer.Models;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace ECommerce.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(Response<NoContent>.Failure(errors, 400));
            }

            return NoContent();
        }
    
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null) return BadRequest();

            return  Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email});
        }
    }
} 