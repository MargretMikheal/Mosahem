using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Queries.GetQuestionsAnswers
{
    public class GetQuestionAnswersQueryValidator : AbstractValidator<GetQuestionAnswersQuery>
    {
        public GetQuestionAnswersQueryValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Opportunities.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
