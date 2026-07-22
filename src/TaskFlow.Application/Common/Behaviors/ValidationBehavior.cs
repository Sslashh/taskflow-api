using FluentValidation;
using MediatR;
using TaskFlow.Application.Common;

namespace TaskFlow.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
            .SelectMany(result => result.Errors)
            .Where(f => f is not null)
            .GroupBy(f => f.PropertyName)
            .Select(g => g.First())
            .ToList();

        if (failures.Count == 0)
            return await next();

        var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));

        // TResponse is constrained to Result, but we still need the concrete
        // Result<T>.Failure(...) when TResponse is a generic Result<T>.
        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var failureMethod = typeof(TResponse).GetMethod(
                nameof(Result.Failure),
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static,
                [typeof(string)]);

            return (TResponse)failureMethod!.Invoke(null, [errorMessage])!;
        }

        return (TResponse)(object)Result.Failure(errorMessage);
    }
}