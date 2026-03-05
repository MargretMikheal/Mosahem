using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Commands.DeleteOrganizationField
{
    public class DeleteOrganizationFieldValidator : AbstractValidator<DeleteOrganizationFieldCommand>
    {
        public DeleteOrganizationFieldValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
