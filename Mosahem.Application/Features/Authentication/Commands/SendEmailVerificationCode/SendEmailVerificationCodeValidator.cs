using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeValidator : AbstractValidator<SendEmailVerificationCodeCommand>
    {
        public SendEmailVerificationCodeValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (email, ct) => await unitOfWork.Users.IsEmailUniqueAsync(email))
                .WithMessage(localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);
        }
    }
}