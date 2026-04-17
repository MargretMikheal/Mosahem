using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Profiles;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerBasicInfoCommand
{
    public class EditVolunteerBasicInfoCommandValidator : AbstractValidator<EditVolunteerBasicInfoCommand>
    {
        public EditVolunteerBasicInfoCommandValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.NationalId)
                .Matches(@"^[0-9]{14}$").WithMessage(localizer[SharedResourcesKeys.Validation.NationalIdMustBe14Digits])
                .MustAsync(async (nationalId, ct) => !await unitOfWork.Repository<Volunteer>()
                    .GetTableNoTracking()
                    .AnyAsync(v => v.NationalId == nationalId, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => !string.IsNullOrWhiteSpace(x.NationalId));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.UtcNow.Date).WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.Gender.HasValue);

            RuleFor(x => x.Bio)
                .MaximumLength(500)
                .WithMessage(localizer[SharedResourcesKeys.Validation.MaxLength])
                .When(x => x.Bio is not null);
        }
    }
}
