using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Users.Commands.ChangeUserEmail.ChangeEmail
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (email, ct) => await unitOfWork.Users.IsEmailUniqueAsync(email))
                .WithMessage(localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
