using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.StopOpportunity
{
    public class StopOpportunityCommandValidator : AbstractValidator<StopOpportunityCommand>
    {
        public StopOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
