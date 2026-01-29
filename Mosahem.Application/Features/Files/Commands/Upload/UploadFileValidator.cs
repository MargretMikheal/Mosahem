using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Files.Commands.Upload;

namespace mosahem.Application.Features.Authentication.Commands.UploadFile
{
    public class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UploadFileValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.File)
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FolderName)
                .NotEmpty()
                .IsEnumName(typeof(StorageFolder), caseSensitive: false)
                .WithMessage(_localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}