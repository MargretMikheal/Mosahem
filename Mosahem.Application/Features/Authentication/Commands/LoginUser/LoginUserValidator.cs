using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using System.Text.RegularExpressions;

namespace mosahem.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.EmailOrPhone)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(input => IsEmail(input) || IsPhone(input)).WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }

        private bool IsEmail(string input) =>
            Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private bool IsPhone(string input) =>
            Regex.IsMatch(input, @"^[0-9]{4,15}$");
    }
}