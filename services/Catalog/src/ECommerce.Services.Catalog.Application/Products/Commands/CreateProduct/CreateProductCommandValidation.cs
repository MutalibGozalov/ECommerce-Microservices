namespace ECommerce.Services.Catalog.Application.Products.Commands.CreateProduct;
public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(p => p.CategoryId)
        .Length(24)
        .WithMessage("Subcategory Id must be length of 24")
        .Matches("^[a-fA-F0-9]{24}$")
        .WithMessage("Subcategory Id contains unsupported characters");

        RuleFor(p => p.Name)
        .NotEmpty()
        .WithMessage("Category Name required")
        .MaximumLength(200)
        .WithMessage("maximum length must be 200");

        RuleForEach(p => p.ProductVariationIds)
        .Length(24)
        .WithMessage("Product Variation Id must be length of 24")
        .Matches("^[a-fA-F0-9]{24}$")
        .WithMessage("Product Variation Id contains unsupported characters");
    }
}