using Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : 
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(f => f.PropertyName)
                .ToDictionary(g => g.Key, g => g.ToArray());

            if (errors.Count != 0)
            {
                var error = Error.Validation( "Validation.Error", "Validation errors have occurred.", errors);

                return Result.CreateFailure<TResponse>(error);
            }
        }

        return await next(cancellationToken);
    }
}