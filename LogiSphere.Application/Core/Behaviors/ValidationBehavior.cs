
using FluentValidation;
using LogiSphere.Application.Core.Errors;
using LogiSphere.Application.Core.Results;
using MediatR;

namespace LogiSphere.Application.Core.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
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
        if(!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
                        .SelectMany(r => r.Errors)
                        .Where(f => f != null)
                        .ToList();

        if(failures.Any())
        {
            var firstFailure = failures.First();
            var error = new Error(firstFailure.ErrorCode ?? "Validation.Error", firstFailure.ErrorMessage);

            if(typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                                    .MakeGenericType(resultType)
                                    .GetMethod(nameof(Result.Failure), [typeof(Error)]);

                return (TResponse)failureMethod!.Invoke(null, [error])!;
            }
            return (TResponse)(object)Result.Failure(error);
        }

        return await next();
    }
}
