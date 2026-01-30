using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.SendOtp
{
    public class SendOtpValidator : AbstractValidator<SendOtpCommand>
    {
        public SendOtpValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}