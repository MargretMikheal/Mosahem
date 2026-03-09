using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.ResumeOpportunity
{
    public class ResumeOpportunityCommandValidator : AbstractValidator<ResumeOpportunityCommand>
    {
        public ResumeOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
