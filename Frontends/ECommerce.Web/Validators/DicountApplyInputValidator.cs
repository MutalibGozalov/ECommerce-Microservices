using ECommerce.Web.Models.Discount;
using FluentValidation;

namespace ECommerce.Web.Validators;
public class DicountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
public DicountApplyInputValidator()
{
    RuleFor(d => d.Code)
    .NotEmpty()
    .WithMessage("Code cannot be empty DARLIN!");
}
}