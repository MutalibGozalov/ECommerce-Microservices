using ECommerce.Web.Models.Catalog;
using FluentValidation;

namespace ECommerce.Web.Validators;
public class ProductCreateInputValidator : AbstractValidator<ProductCreateInput>
{
    public ProductCreateInputValidator()
    {
        RuleFor(p => p.Name)
        .NotEmpty()
        .WithMessage("Product Name can not be null DARLIN! ");

        RuleFor(p => p.Description)
        .NotEmpty()
        .WithMessage("Description can not be null DARLIN!");

        RuleFor(p => p.DisplayPrice)
        .NotEmpty()
        .WithMessage("Price can not be null DARLIN!")
        .PrecisionScale(2, 6, true)
        .WithMessage("Incorrect price, coreect form: $$$$.$$");

        RuleFor(p => p.StoreId)
        .NotEmpty()
        .WithMessage("Store Id can not be null DARLIN!");

    }
}