using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.CommentOpportunity
{
    public class CommentOpportunityCommandValidator : AbstractValidator<CommentOpportunityCommand>
    {
        public CommentOpportunityCommandValidator(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)
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

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(text => !string.IsNullOrWhiteSpace(text))
                .WithMessage(localizer[SharedResourcesKeys.Validation.CannotBeEmptyOrWhitespace])
                .MaximumLength(1000).WithMessage(localizer[SharedResourcesKeys.Validation.MaxLength]);
        }
    }
}
