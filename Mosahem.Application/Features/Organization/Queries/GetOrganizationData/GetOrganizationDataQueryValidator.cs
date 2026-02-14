using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationData
{
    public class GetOrganizationDataQueryValidator : AbstractValidator<GetOrganizationDataQuery>
    {
        public GetOrganizationDataQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Organizations.GetByIdAsync(id, ct) != null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
