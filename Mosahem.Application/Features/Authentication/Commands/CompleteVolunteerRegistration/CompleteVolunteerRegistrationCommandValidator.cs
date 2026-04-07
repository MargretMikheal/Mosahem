using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration;

namespace mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration
{
    public class CompleteVolunteerRegistrationCommandValidator : AbstractValidator<CompleteVolunteerRegistrationCommand>
    {
        public CompleteVolunteerRegistrationCommandValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"[a-zA-Z\u0600-\u06FF]").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (email, ct) => await unitOfWork.Users.IsEmailUniqueAsync(email))
                .WithMessage(localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Matches(@"^[0-9]{4,15}$").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (phone, ct) => await unitOfWork.Users.IsPhoneUniqueAsync(phone))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MinimumLength(6).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MinLength], 6))
                .Matches(@"[A-Z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresUpper])
                .Matches(@"[a-z]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresLower])
                .Matches(@"[0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresDigit])
                .Matches(@"[^a-zA-Z0-9]").WithMessage(localizer[SharedResourcesKeys.Validation.PasswordRequiresNonAlphanumeric]);

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.UtcNow.Date).WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.Gender.HasValue);

            RuleFor(x => x.NationalId)
                .Matches(@"^[0-9]{14}$").WithMessage(localizer[SharedResourcesKeys.Validation.NationalIdMustBe14Digits])
                .MustAsync(async (nationalId, ct) => !await unitOfWork.Repository<Volunteer>()
                    .GetTableNoTracking()
                    .AnyAsync(v => v.NationalId == nationalId, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => !string.IsNullOrWhiteSpace(x.NationalId));

            RuleFor(x => x.FieldIds)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleForEach(x => x.FieldIds)
                .MustAsync(async (id, ct) => await unitOfWork.Fields.GetByIdAsync(id, ct) != null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.SkillIds)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry])
                .MustAsync(async (ids, ct) => await unitOfWork.Skills.AreAllExistingAsync(ids, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            When(x => x.Location is not null, () =>
            {
                RuleFor(x => x.Location!.GovernorateId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                    .MustAsync(async (id, ct) => await unitOfWork.Governorates.GetByIdAsync(id, ct) != null)
                    .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

                RuleFor(x => x.Location!.CityId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                    .MustAsync(async (command, cityId, ct) =>
                    {
                        var city = await unitOfWork.Repository<City>().GetByIdAsync(cityId, ct);
                        if (city == null) return false;

                        return city.GovernorateId == command.Location!.GovernorateId;
                    })
                    .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
            });
        }
    }
}
