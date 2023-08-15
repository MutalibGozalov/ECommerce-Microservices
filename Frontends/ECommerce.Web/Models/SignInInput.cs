using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;
public class SigninInput
{
    [Required]
    [Display(Name = "Email address")]
    public string? Email { get; set; }
    [Required]
    [Display(Name = "Password")]
    public string? Password { get; set; }
    [Required]
    [Display(Name = "Remember me")]
    public bool IsRememberMe { get; set; }
}