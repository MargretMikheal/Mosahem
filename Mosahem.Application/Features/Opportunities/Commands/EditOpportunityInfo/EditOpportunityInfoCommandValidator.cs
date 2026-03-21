using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityInfo
{
    public class EditOpportunityInfoCommandValidator : AbstractValidator<EditOpportunityInfoCommand>
    {
        public EditOpportunityInfoCommandValidator(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.OpportunityId)
            .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OrganizationId)
            .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MaximumLength(100).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 100))
                .When(x => x.Title is not null);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MaximumLength(5000).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 5000))
                .When(x => x.Description is not null);

            RuleFor(x => x.Vacancies)
                .GreaterThan(10).WithMessage(localizer[SharedResourcesKeys.Validation.VacanciesMinimumValueIs10])
                .When(x => x.Vacancies.HasValue);

            RuleFor(x => x.Addresses)
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
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
              .When(x => x.Addresses is not null);
        }
    }
}
