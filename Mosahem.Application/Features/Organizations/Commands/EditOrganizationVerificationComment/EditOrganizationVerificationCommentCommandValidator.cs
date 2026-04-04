using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationVerificationComment
{
    public class EditOrganizationVerificationCommentCommandValidator : AbstractValidator<EditOrganizationVerificationCommentCommand>
    {
        public EditOrganizationVerificationCommentCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.VerificationComment)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
