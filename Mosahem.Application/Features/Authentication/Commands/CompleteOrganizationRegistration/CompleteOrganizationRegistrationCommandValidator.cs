using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Enums;
using Mosahem.Domain.Entities;

namespace mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration
{
    public class CompleteOrganizationRegistrationCommandValidator : AbstractValidator<CompleteOrganizationRegistrationCommand>
    {
        public CompleteOrganizationRegistrationCommandValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationName)
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

            RuleFor(x => x.FieldIds)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleForEach(x => x.FieldIds)
                .MustAsync(async (id, ct) => await unitOfWork.Fields.GetByIdAsync(id, ct) != null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.Addresses)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(Addresses =>
                {
                    if (Addresses == null || !Addresses.Any()) return true;

                    var duplicates = Addresses
                        .GroupBy(l => new { l.GovernorateId, l.CityId })
                        .Where(g => g.Count() > 1);

                    return !duplicates.Any();
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleForEach(x => x.Addresses).ChildRules(location =>
            {
                location.RuleFor(l => l.GovernorateId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                    .MustAsync(async (id, ct) => await unitOfWork.Governorates.GetByIdAsync(id, ct) != null)
                    .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

                location.RuleFor(l => l.CityId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                    .MustAsync(async (dto, cityId, ct) =>
                    {
                        var city = await unitOfWork.Repository<City>().GetByIdAsync(cityId, ct);
                        if (city == null) return false;

                        return city.GovernorateId == dto.GovernorateId;
                    })
                    .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
            });

            When(x => !string.IsNullOrWhiteSpace(x.LicenseUrl), () =>
            {
                RuleFor(x => x.LicenseUrl!)
                    .MustAsync(async (licenseKey, cancellationToken) =>
                        await unitOfWork.Repository<TemporaryFileUpload>()
                            .GetTableNoTracking()
                            .AnyAsync(x => x.FileKey == licenseKey && x.FolderName == StorageFolder.Licenses.ToString(), cancellationToken))
                    .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
            });
        }
    }
}
