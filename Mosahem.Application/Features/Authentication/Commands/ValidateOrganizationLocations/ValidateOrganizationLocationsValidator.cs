using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

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
            RuleForEach(x => x.Locations).ChildRules(address =>
            {
                address.RuleFor(x => x.GovernorateId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

                address.RuleFor(x => x.CityId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

                address.RuleFor(x => x.Details)
                    .MaximumLength(500).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 500));
            });
            RuleFor(x => x.Locations)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(addresses => addresses.Select(a => a.CityId).Distinct().Count() == addresses.Count)
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry])
                .MustAsync(async (addresses, ct) =>
                {
                    var cityGovernoratePairs = addresses.ToDictionary(a => a.CityId, a => a.GovernorateId);
                    return await unitOfWork.Cities.AreValidCityGovernoratePairsAsync(
                        cityGovernoratePairs.Keys.ToList(),
                        cityGovernoratePairs,
                        ct);
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

        }
    }
}