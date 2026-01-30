using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}