
namespace ECommerce.Services.Order.Application.Order.Commands;
public class CreateOrderCommandValidation: AbstractValidator<CreateOrder>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(p => p.UserId)
        .NotEmpty()
        .WithMessage("User Id cannot be null DARLIN!");
    }
    
}