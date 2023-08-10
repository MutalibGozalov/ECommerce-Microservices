
namespace ECommerce.Services.Order.Application.Order.Commands;
public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(o => o.UserId)
        .NotEmpty()
        .WithMessage("User Id cannot be null DARLIN!");
    }
    
}