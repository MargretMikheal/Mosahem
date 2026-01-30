using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Admin.Commands.AddAdmin
{
    public class AddAdminValidator : AbstractValidator<AddAdminCommand>
    {
        public AddAdminValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .EmailAddress()
                .MustAsync(async (email, ct) => await unitOfWork.Users.IsEmailUniqueAsync(email))
                .WithMessage(localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (phone, ct) => await unitOfWork.Users.IsPhoneUniqueAsync(phone))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists]);

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage(localizer[SharedResourcesKeys.User.PasswordsDoNotMatch]);
        }
    }
}