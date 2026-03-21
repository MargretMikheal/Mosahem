using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunitySkills
{
    public class EditOpportunitySkillsCommandValidator : AbstractValidator<EditOpportunitySkillsCommand>
    {
        public EditOpportunitySkillsCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.SkillType)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .IsEnumName(typeof(OpportunitySkillType), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.SkillsIds)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
