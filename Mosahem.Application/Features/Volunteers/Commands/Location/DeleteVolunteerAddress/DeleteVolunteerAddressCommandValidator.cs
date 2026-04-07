using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.Location.DeleteVolunteerAddress
{
    public class DeleteVolunteerAddressCommandValidator : AbstractValidator<DeleteVolunteerAddressCommand>
    {
        public DeleteVolunteerAddressCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
