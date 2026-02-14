using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations
{
    public class GetOrganizationLocationsQueryValidator : AbstractValidator<GetOrganizationLocationsQuery>
    {
        public GetOrganizationLocationsQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Organizations.GetByIdAsync(id, ct) != null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
