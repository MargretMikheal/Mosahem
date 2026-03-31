using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Admin.Commands.EditBasicInfo
{
    public class EditAdminInfoValidator : AbstractValidator<EditAdminInfoCommand>
    {
        public EditAdminInfoValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MustAsync(async (model, key, ct) => await unitOfWork.Users.IsNameUniqueExcludeSelfAsync(model.AdminId, key, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.UserName is not null);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^[0-9]{4,15}$").WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (phone, ct) => await unitOfWork.Users.IsPhoneUniqueAsync(phone))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);
        }
    }
}
