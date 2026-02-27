using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress
{
    public class AddOrganizationAddressValidator : AbstractValidator<AddOrganizationAddressCommand>
    {
        public AddOrganizationAddressValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Organizations.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.GovernateId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Governorates.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.CityID)
                 .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                 .MustAsync(async (model, cityId, ct) => await unitOfWork.Cities.IsExistByGovernateAsync(model.GovernateId, cityId, ct))
                 .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);



        }
    }
}
