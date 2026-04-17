using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.AcceptApplicantCommand
{
    public class AcceptApplicantCommandValidator : AbstractValidator<AcceptApplicantCommand>
    {
        public AcceptApplicantCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);


            RuleFor(x => x.ApplicantId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
