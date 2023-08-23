using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;
    private readonly HttpClient _httpClient;

    public AuthController(ILogger<AuthController> logger, IIdentityService identityService, IUserService userService, HttpClient httpClient)
    {
        _logger = logger;
        _identityService = identityService;
        _userService = userService;
        _httpClient = httpClient;
    }

    [HttpGet("SignIn")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SigninInput signinInput)
    {
        if (ModelState.IsValid is false)
        {
            return View();
        }

        var response = await _identityService.SignIn(signinInput);

        if (response.IsSuccessful is false)
        {
            response.Errors.ForEach(e =>
            {
                ModelState.AddModelError(String.Empty, e);
            });
            return View();
        }

        return RedirectToAction("Index", "Home");
    }



    [HttpGet("SignUp")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpInput signUpInput)
    {
        if (ModelState.IsValid is false)
        {
            return View();
        }

        var response = await _identityService.SignUp(signUpInput);

        if (response is false)
        {
            return View();
        }

        return RedirectToAction("SignIn");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        await _identityService.RevokeRefreshToken();

        return RedirectToAction("Index", "Home");
    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}