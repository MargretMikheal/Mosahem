using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByVerificationStatus
{
    public class GetOpportunitiesByVerificationStatusQueryValidator : AbstractValidator<GetOpportunitiesByVerificationStatusQuery>
    {
        public GetOpportunitiesByVerificationStatusQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityVerificationStatus)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .IsEnumName(typeof(VerficationStatus), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageNumberMustBeGreaterThanZero]);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(500).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageSizeMustNotExceedMax]);
        }
    }
}
