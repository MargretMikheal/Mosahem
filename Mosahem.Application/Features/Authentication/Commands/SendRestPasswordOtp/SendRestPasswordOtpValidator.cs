using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.SendRestPasswordOtp
{
    public class SendRestPasswordOtpValidator : AbstractValidator<SendRestPasswordOtpCommand>
    {
        public SendRestPasswordOtpValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}