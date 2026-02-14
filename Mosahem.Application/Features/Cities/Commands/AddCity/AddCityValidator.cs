using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Cities.Commands.AddCity
{
    public class AddCityValidator : AbstractValidator<AddCityCommand>
    {
        public AddCityValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.GovernorateId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Cities.IsExistByNameInGovernateAsync(model.GovernorateId, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Cities.IsExistByNameInGovernateAsync(model.GovernorateId, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);
        }
    }
}
