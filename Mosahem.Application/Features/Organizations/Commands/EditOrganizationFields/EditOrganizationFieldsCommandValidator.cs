using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationFields
{
    public class EditOrganizationFieldsCommandValidator : AbstractValidator<EditOrganizationFieldsCommand>
    {
        public EditOrganizationFieldsCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
            .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FieldsIds)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
