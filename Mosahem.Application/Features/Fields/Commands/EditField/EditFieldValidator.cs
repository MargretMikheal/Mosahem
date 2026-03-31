using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Fields.Commands.EditField
{
    public class EditFieldValidator : AbstractValidator<EditFieldCommand>
    {
        public EditFieldValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Fields.IsExistByNameExcludeSelfAsync(model.Id, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.NameAr is not null);

            RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Fields.IsExistByNameExcludeSelfAsync(model.Id, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.NameEn is not null);
        }
    }
}
