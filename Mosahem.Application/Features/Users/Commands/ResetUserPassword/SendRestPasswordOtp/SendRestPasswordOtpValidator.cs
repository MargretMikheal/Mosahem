using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.SendRestPasswordOtp
{
    public class SendRestPasswordOtpValidator : AbstractValidator<SendRestPasswordOtpCommand>
    {
        public SendRestPasswordOtpValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}