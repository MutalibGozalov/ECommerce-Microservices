using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;
public class SigninInput
{
    [Display(Name = "Email address")]
    public string? Email { get; set; }
    [Display(Name = "Password")]
    public string? Password { get; set; }
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}