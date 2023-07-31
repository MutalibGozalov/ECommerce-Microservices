using System.Text.RegularExpressions;

namespace ECommerce.Services.Catalog.Application.Categories.Commands.UpdateCategory;
 public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
 {
     public UpdateCategoryCommandValidation()
     {
        RuleFor(c => c.Id)
        .Length(24)
        .WithMessage("Subcategory Id must be length of 24")
        //.Must(BeGuid)
        .Matches("^[a-fA-F0-9]{24}$")
        .WithMessage("Subcategory Id contains unsupported characters");

        RuleFor(c => c.Name)
        .NotEmpty()
        .WithMessage("Category Name required")
        .MaximumLength(200)
        .WithMessage("maximum length must be 200");

        RuleFor(c => c.SubcategoryId)
        .Length(24)
        .WithMessage("Subcategory Id must be length of 24")
        //.Must(BeGuid)
        .Matches("^[a-fA-F0-9]{24}$")
        .WithMessage("Subcategory Id contains unsupported characters");
        
     }
    //public bool BeGuid(string id) => Regex.IsMatch(id, "^[a-fA-F0-9]{24}$");
 }