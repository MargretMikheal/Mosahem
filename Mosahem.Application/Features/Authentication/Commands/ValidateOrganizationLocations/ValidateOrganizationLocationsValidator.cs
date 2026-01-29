using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;

namespace mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations
{
    public class ValidateOrganizationLocationsValidator : AbstractValidator<ValidateOrganizationLocationsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateOrganizationLocationsValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(x => x.Locations)
                .NotNull().NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required]);

            RuleForEach(x => x.Locations).ChildRules(location =>
            {

                location.RuleFor(l => l.GovernorateId)
                    .NotEmpty()
                    .MustAsync(async (id, ct) => await _unitOfWork.Governorates.GetByIdAsync(id) != null)
                    .WithMessage(_localizer[SharedResourcesKeys.Validation.NotFound]);

                location.RuleFor(l => l.CityId)
                    .NotEmpty()
                    .MustAsync(async (dto, cityId, ct) =>
                    {
                        var city = await _unitOfWork.Repository<City>().GetByIdAsync(cityId);
                        if (city == null) return false;

                        return city.GovernorateId == dto.GovernorateId;
                    })
                    .WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid]); 

            });
        }
    }
}