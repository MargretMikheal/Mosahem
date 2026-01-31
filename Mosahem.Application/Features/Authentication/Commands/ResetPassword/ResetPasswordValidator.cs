using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty()
                .MinimumLength(6).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MinLength], 6))
                  .Matches(@"[A-Z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresUpper])
                  .Matches(@"[a-z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresLower])
                  .Matches(@"[0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresDigit])
                  .Matches(@"[^a-zA-Z0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresNonAlphanumeric]);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword)
                .WithMessage(localizer[SharedResourcesKeys.User.PasswordsDoNotMatch]);
        }
    }
}
