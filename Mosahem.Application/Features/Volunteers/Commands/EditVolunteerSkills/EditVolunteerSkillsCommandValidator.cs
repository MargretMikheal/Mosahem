using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerSkills
{
    public class EditVolunteerSkillsCommandValidator : AbstractValidator<EditVolunteerSkillsCommand>
    {
        public EditVolunteerSkillsCommandValidator(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.SkillIds)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (ids, cancellationToken) => await unitOfWork.Skills.AreAllExistingAsync(ids, cancellationToken))
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}
