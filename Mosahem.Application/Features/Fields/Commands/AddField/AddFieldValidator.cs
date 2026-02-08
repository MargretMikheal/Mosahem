using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Fields.Commands.AddField
{
    public class AddFieldValidator : AbstractValidator<AddFieldCommand>
    {
        public AddFieldValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (key, ct) => !await unitOfWork.Fields.IsExistByNameAsync(key))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (key, ct) => !await unitOfWork.Fields.IsExistByNameAsync(key))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);
        }
    }
}
