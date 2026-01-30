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
                 .MinimumLength(6).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MinLength], 6));

            RuleFor(x => x.ConfirmNewPassword)
                 .Equal(x => x.NewPassword).WithMessage(localizer[SharedResourcesKeys.User.PasswordsDoNotMatch]);
        }
    }
}