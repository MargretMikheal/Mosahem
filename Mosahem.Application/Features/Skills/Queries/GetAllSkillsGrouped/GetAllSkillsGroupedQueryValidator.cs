using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped
{
    public class GetAllSkillsGroupedQueryValidator : AbstractValidator<GetAllSkillsGroupedQuery>
    {
        public GetAllSkillsGroupedQueryValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.FieldIds)
                .Must(fieldIds => fieldIds is null || fieldIds.All(id => id != Guid.Empty))
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.FieldIds)
                .Must(fieldIds => fieldIds is null || fieldIds.Distinct().Count() == fieldIds.Count)
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleFor(x => x.FieldIds)
                .MustAsync(async (fieldIds, ct) =>
                {
                    if (fieldIds is null || fieldIds.Count == 0)
                        return true;

                    return await unitOfWork.Fields.AreAllExistingAsync(fieldIds.Distinct().ToList(), ct);
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
