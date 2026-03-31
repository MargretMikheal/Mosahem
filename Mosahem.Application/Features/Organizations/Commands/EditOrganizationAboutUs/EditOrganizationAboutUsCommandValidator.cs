using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationAboutUs
{
    public class EditOrganizationAboutUsCommandValidator : AbstractValidator<EditOrganizationAboutUsCommand>
    {
        public EditOrganizationAboutUsCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.AboutUs)
                .MaximumLength(1000).WithMessage(localizer[SharedResourcesKeys.Validation.MaxLength])
                .When(x => !string.IsNullOrEmpty(x.AboutUs));
        }
    }
}
