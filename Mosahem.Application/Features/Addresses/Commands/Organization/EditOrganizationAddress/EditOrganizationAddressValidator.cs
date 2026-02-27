using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress
{
    public class EditOrganizationAddressValidator : AbstractValidator<EditOrganizationAddressCommand>
    {
        public EditOrganizationAddressValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.AddressId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .When(x => x.GovernateId.HasValue);
        }
    }
}
