using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Volunteers.Commands.FollowOrganization
{
    public class FollowOrganizationCommandValidator : AbstractValidator<FollowOrganizationCommand>
    {
        public FollowOrganizationCommandValidator(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Volunteers.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Organizations.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x)
                .MustAsync(async (model, ct) => !await unitOfWork.Repository<OrganizationFollower>()
                    .GetTableNoTracking()
                    .AnyAsync(f => f.VolunteerId == model.VolunteerId && f.OrganizationId == model.OrganizationId, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.VolunteerId != Guid.Empty && x.OrganizationId != Guid.Empty);
        }
    }
}
