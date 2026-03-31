using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationLicense
{
    public class GetOrganizationLicenseQueryValidator : AbstractValidator<GetOrganizationLicenseQuery>
    {
        public GetOrganizationLicenseQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (key, cancellation) => await unitOfWork.Organizations.GetByIdAsync(key, cancellation) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
