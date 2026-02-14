using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Users.GetByIdAsync(id, ct) != null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
