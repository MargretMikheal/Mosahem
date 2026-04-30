using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.LikeOpportunity
{
    public class LikeOpportunityCommandValidator : AbstractValidator<LikeOpportunityCommand>
    {
        public LikeOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Volunteers.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.OpportunityId)
                .MustAsync(async (id, ct) => await unitOfWork.Opportunities
                    .GetTableNoTracking()
                    .AnyAsync(opportunity => opportunity.Id == id && opportunity.VerificationStatus == VerficationStatus.Approved, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.OpportunityMustBeVerified])
                .When(x => x.OpportunityId != Guid.Empty);

            RuleFor(x => x)
                .MustAsync(async (model, ct) => !await unitOfWork.Repository<OpportunityLike>()
                    .GetTableNoTracking()
                    .AnyAsync(l => l.VolunteerId == model.VolunteerId && l.OpportunityId == model.OpportunityId, ct))
                .WithMessage(localizer[SharedResourcesKeys.State.AlreadyExists])
                .When(x => x.VolunteerId != Guid.Empty && x.OpportunityId != Guid.Empty);
        }
    }
}
