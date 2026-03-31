using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.CurrentPassword)
                 .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.NewPassword)
                  .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                  .MinimumLength(6).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MinLength], 6))
                  .Matches(@"[A-Z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresUpper])
                  .Matches(@"[a-z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresLower])
                  .Matches(@"[0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresDigit])
                  .Matches(@"[^a-zA-Z0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresNonAlphanumeric]);

            RuleFor(x => x.ConfirmNewPassword)
                 .Equal(x => x.NewPassword).WithMessage(localizer[SharedResourcesKeys.User.PasswordsDoNotMatch]);
        }
    }
}