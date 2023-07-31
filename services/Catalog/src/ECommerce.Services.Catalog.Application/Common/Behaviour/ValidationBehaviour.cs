
namespace ECommerce.Services.Catalog.Application.Common.Behaviour;
public class ValidationBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult> where TRequest : class 
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) 
    {
        _validators = validators;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var ctx = new ValidationContext<TRequest>(request);
        var validationResult = _validators
        .Select(v => v.Validate(ctx))
        .SelectMany(v => v.Errors)
        .Where(x => x is not null)
        .ToList();
        if (validationResult.Any())
            throw new ValidationException(validationResult);
        return await next();
    }
}