using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields
{
    public class EditVolunteerFieldsCommandValidator : AbstractValidator<EditVolunteerFieldsCommand>
    {
        public EditVolunteerFieldsCommandValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.FieldIds)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (ids, cancellationToken) => await unitOfWork.Fields.AreAllExistingAsync(ids, cancellationToken))
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}
