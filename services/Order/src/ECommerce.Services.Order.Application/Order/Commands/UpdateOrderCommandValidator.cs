
namespace ECommerce.Services.Order.Application.Order.Commands;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.UserId)
        .NotEmpty()
        .WithMessage("User Id of order can not be empty DARLIN!");
    }
}