using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById
{
    public class GetOpportunityByIdQueryValidator : AbstractValidator<GetOpportunityByIdQuery>
    {
        public GetOpportunityByIdQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, cancellationToken) => await unitOfWork.Opportunities.GetByIdAsync(id, cancellationToken) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
