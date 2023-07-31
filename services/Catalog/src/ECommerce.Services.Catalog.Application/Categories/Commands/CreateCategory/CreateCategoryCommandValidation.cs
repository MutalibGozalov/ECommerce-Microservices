namespace ECommerce.Services.Catalog.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
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
        .WithMessage("Subcategory Id contains unsupported characters LLALA");
    }

    //public bool BeGuid(string id) => Regex.IsMatch(id, "^[a-fA-F0-9]{24}$");
}

/*
/^[a-fA-F0-9]{24}$/
*/