using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Profiles;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateVolunteerBasicInfo
{
    public class ValidateVolunteerBasicInfoValidator : AbstractValidator<ValidateVolunteerBasicInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateVolunteerBasicInfoValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"[a-zA-Z\u0600-\u06FF]").WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid]);

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

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.UtcNow.Date).WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.Gender.HasValue);

            RuleFor(x => x.NationalId)
                .Matches(@"^[0-9]{14}$").WithMessage(_localizer[SharedResourcesKeys.Validation.NationalIdMustBe14Digits])
                .MustAsync(async (nationalId, ct) => !await _unitOfWork.Repository<Volunteer>()
                    .GetTableNoTracking()
                    .AnyAsync(v => v.NationalId == nationalId, ct))
                .WithMessage(_localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => !string.IsNullOrWhiteSpace(x.NationalId));
        }
    }
}
