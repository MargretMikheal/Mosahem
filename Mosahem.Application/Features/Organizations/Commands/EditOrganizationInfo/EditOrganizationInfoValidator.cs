using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationInfo
{
    public class EditOrganizationInfoValidator : AbstractValidator<EditOrganizationInfoCommand>
    {
        public EditOrganizationInfoValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, CancellationToken) => await unitOfWork.Organizations.GetByIdAsync(id, CancellationToken) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .Matches(@"[a-zA-Z\u0600-\u06FF]").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .Must(name => !name.All(char.IsDigit)).WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (model, key, CancellationToken) => await unitOfWork.Users.IsNameUniqueExcludeSelfAsync(model.OrganizationId, key, CancellationToken))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.OrganizationName is not null);

            RuleFor(x => x.OrganizationPhoneNumber)
                .Matches(@"^[0-9]{4,15}$").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (phone, ct) => await unitOfWork.Users.IsPhoneUniqueAsync(phone))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

        }
    }
}
