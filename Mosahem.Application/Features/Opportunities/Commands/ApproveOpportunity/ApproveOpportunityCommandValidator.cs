using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.ApproveOpportunity
{
    public class ApproveOpportunityCommandValidator : AbstractValidator<ApproveOpportunityCommand>
    {
        public ApproveOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
