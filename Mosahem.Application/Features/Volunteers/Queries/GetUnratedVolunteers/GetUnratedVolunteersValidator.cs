using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Queries.GetUnratedVolunteers
{
    public class GetUnratedVolunteersValidator : AbstractValidator<GetUnratedVolunteersQuery>
    {
        public GetUnratedVolunteersValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Page)
               .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageNumberMustBeGreaterThanZero]);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(500).WithMessage(localizer[SharedResourcesKeys.Validation.Pagination.PageSizeMustNotExceedMax]);
        }
    }
}
