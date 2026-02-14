using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.ValidateBasicInfo
{
    public class ValidateBasicInfoValidator : AbstractValidator<ValidateBasicInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateBasicInfoValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"[a-zA-Z\u0600-\u06FF]").WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid])
                .Must(name => !name.All(char.IsDigit)).WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (email, ct) => await _unitOfWork.Users.IsEmailUniqueAsync(email))
                .WithMessage(_localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"^[0-9]{4,15}$").WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (phone, ct) => await _unitOfWork.Users.IsPhoneUniqueAsync(phone))
                .WithMessage(_localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .MinimumLength(6).WithMessage(string.Format(_localizer[SharedResourcesKeys.Validation.MinLength], 6))
                .Matches(@"[A-Z]").WithMessage(_localizer[SharedResourcesKeys.Validation.PasswordRequiresUpper])
                .Matches(@"[a-z]").WithMessage(_localizer[SharedResourcesKeys.Validation.PasswordRequiresLower])
                .Matches(@"[0-9]").WithMessage(_localizer[SharedResourcesKeys.Validation.PasswordRequiresDigit])
                .Matches(@"[^a-zA-Z0-9]").WithMessage(_localizer[SharedResourcesKeys.Validation.PasswordRequiresNonAlphanumeric]);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.User.PasswordsDoNotMatch]);
        }
    }
}