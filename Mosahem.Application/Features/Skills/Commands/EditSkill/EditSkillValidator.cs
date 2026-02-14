using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Skills.Commands.EditSkill
{
    public class EditSkillValidator : AbstractValidator<EditSkillCommand>
    {
        public EditSkillValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.NameAr)
                .Must(s => s is null || !string.IsNullOrWhiteSpace(s))
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Skills.IsExistByNameExcludeSelfAsync(model.Id, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.NameEn)
                .Must(s => s is null || !string.IsNullOrWhiteSpace(s))
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MustAsync(async (model, key, ct) => !await unitOfWork.Skills.IsExistByNameExcludeSelfAsync(model.Id, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.Category)
                .Must(s => s is null || !string.IsNullOrWhiteSpace(s))
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace]);
        }
    }
}
