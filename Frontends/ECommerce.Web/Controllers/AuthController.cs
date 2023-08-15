using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IIdentityService _identityService;

    public AuthController(ILogger<AuthController> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(SigninInput signinInput)
    {
        if(ModelState.IsValid is false)
        {
            return View();
        }

        var response = await _identityService.SignIn(signinInput);

        if(response.IsSuccessful is false)
        {
            response.Errors.ForEach(e => {
                ModelState.AddModelError(String.Empty, e);
            });
            return View(); 
        }

       return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}