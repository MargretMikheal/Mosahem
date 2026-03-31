using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditOrganizationFile
{
    public class EditOrganizationFileCommandValidator : AbstractValidator<EditOrganizationFileCommand>
    {
        public EditOrganizationFileCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FolderName)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .IsEnumName(typeof(StorageFolder), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.File)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
