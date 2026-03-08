using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditVolunteerFile
{
    public class EditVolunteerFileCommandValidator : AbstractValidator<EditVolunteerFileCommand>
    {
        public EditVolunteerFileCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FolderName)
                .NotEmpty()
                .IsEnumName(typeof(StorageFolder), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.File)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
