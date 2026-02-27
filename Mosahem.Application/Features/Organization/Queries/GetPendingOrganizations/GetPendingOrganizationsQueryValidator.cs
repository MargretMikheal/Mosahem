using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsQueryValidator : AbstractValidator<GetPendingOrganizationsQuery>
    {
        public GetPendingOrganizationsQueryValidator(IStringLocalizer<SharedResources> localizers)
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage(localizers[SharedResourcesKeys.Validation.Pagination.PageNumberMustBeGreaterThanZero]);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(50).WithMessage(localizers[SharedResourcesKeys.Validation.Pagination.PageSizeMustNotExceedMax]);
        }
    }
}
