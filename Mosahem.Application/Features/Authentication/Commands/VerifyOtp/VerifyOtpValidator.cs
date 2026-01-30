using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Authentication.Commands.VerifyOtp;

namespace mosahem.Application.Features.Authentication.Commands.VerifyOtp
{
    public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Length(6).WithMessage("Code must be 6 digits.");
        }
    }
}