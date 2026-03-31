using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByStatus
{
    public class GetOpportunitiesByStatusQueryValidator : AbstractValidator<GetOpportunitiesByStatusQuery>
    {
        public GetOpportunitiesByStatusQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityStatus)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .IsEnumName(typeof(OpportunityStatus), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Page)
               .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageNumberMustBeGreaterThanZero]);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(500).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageSizeMustNotExceedMax]);
        }
    }
}
