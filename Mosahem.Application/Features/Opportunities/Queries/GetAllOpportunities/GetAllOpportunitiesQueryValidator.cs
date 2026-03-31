using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunities
{
    public class GetAllOpportunitiesQueryValidator : AbstractValidator<GetAllOpportunitiesQuery>
    {
        public GetAllOpportunitiesQueryValidator(IStringLocalizer<SharedResources> localizer)
        {

            RuleFor(x => x.OpportunityStatus)
                .IsEnumName(typeof(OpportunityStatus), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => !string.IsNullOrWhiteSpace(x.OpportunityStatus));

            RuleFor(x => x.WorkType)
                .IsEnumName(typeof(OpportunityWorkType), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => !string.IsNullOrWhiteSpace(x.WorkType));

            RuleFor(x => x.LocationType)
                .IsEnumName(typeof(OpportunityLocationType), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => !string.IsNullOrWhiteSpace(x.LocationType));

            RuleFor(x => x.FieldIds)
                .Must(fieldIds => fieldIds!.Count() <= 3).WithMessage(localizer[SharedResourcesKeys.Validation.FieldsMustNotExceedThree])
                .When(x => x.FieldIds != null);

            RuleFor(x => x.RequiredSkillIds)
                .Must(requiredSkillIds => requiredSkillIds!.Count() <= 3).WithMessage(localizer[SharedResourcesKeys.Validation.SkillsMustNotExceedThree])
                .When(x => x.RequiredSkillIds != null);

            RuleFor(x => x.ProvidedSkillIds)
                .Must(providedSkillIds => providedSkillIds!.Count() <= 3).WithMessage(localizer[SharedResourcesKeys.Validation.SkillsMustNotExceedThree])
                .When(x => x.ProvidedSkillIds != null);

            RuleFor(x => x.Page)
               .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageNumberMustBeGreaterThanZero]);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(500).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageSizeMustNotExceedMax]);

        }
    }
}
