using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus
{
    public class GetApplicantsByStatusQueryValidator
        : AbstractValidator<GetApplicantsByStatusQuery>
    {
        public GetApplicantsByStatusQueryValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Opportunities.GetByIdAsync(id, ct) is not null)

                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.Status)
                .IsEnumName(typeof(ApplicantStatus), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}
