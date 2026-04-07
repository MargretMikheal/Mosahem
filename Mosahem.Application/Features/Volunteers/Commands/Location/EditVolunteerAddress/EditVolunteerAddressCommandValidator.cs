using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.Location.EditVolunteerAddress
{
    public class EditVolunteerAddressCommandValidator : AbstractValidator<EditVolunteerAddressCommand>
    {
        public EditVolunteerAddressCommandValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .When(x => x.GovernateId.HasValue);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MaximumLength(500).WithMessage(localizer[SharedResourcesKeys.Validation.MaxLength]);
        }
    }
}
