using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;
using mosahem.Application.Exceptions;

namespace mosahem.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
                                  IStringLocalizer<SharedResources> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errorDict = failures
                        .GroupBy(f => string.IsNullOrWhiteSpace(f.PropertyName) ? "_" : f.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(f =>
                            {
                                var localized = _localizer?[f.ErrorMessage]?.Value;
                                return string.IsNullOrEmpty(localized) ? f.ErrorMessage : localized;
                            }).ToList()
                        );

                    var message = _localizer?[SharedResourcesKeys.Validation.UnprocessableEntity] ?? "Unprocessable entity";
                    throw new ValidationExceptionWithErrors(message, errorDict);
                }
            }

            return await next();
        }
    }
}
