using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Bookify.Application.Behaviors;
internal class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(request))
            .Where(result => result.Errors.Any())
            .SelectMany(result => result.Errors)
            .Select(error => new ValidationError(error.PropertyName, error.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next(cancellationToken);

    }
}
