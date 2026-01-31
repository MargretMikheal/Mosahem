using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Authentication.Commands.VerifyRestPasswordOtp;

namespace mosahem.Application.Features.Authentication.Commands.VerifyRestPasswordOtp
{
    public class VerifyRestPasswordOtpValidator : AbstractValidator<VerifyRestPasswordOtpCommand>
    {
        public VerifyRestPasswordOtpValidator(IStringLocalizer<SharedResources> localizer)
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