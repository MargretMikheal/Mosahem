using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.RateApplicant
{
    public class RateApplicantValidator : AbstractValidator<RateApplicantCommand>
    {
        public RateApplicantValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5).WithMessage(localizer[SharedResourcesKeys.Validation.OutOfRange]);
        }
    }
}
