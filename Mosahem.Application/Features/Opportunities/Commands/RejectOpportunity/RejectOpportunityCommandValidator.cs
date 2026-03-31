using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.RejectOpportunity
{
    public class RejectOpportunityCommandValidator : AbstractValidator<RejectOpportunityCommand>
    {
        public RejectOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
