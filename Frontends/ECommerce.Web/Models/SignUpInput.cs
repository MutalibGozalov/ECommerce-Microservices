using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;
public class SignUpInput
{
public string? Username { get; set; }
[Required]
[Display(Name = "Email address")]
public string? Email { get; set; }
[Required]
[Display(Name = "Password")]
public string? Password { get; set; }
}