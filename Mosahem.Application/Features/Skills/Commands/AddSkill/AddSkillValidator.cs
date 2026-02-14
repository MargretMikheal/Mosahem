using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillValidator : AbstractValidator<AddSkillCommand>
    {
        public AddSkillValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (key, ct) => !await unitOfWork.Skills.IsExistByNameAsync(key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (key, ct) => !await unitOfWork.Skills.IsExistByNameAsync(key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
