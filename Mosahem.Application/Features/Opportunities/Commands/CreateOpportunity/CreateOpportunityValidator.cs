using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity
{
    public class CreateOpportunityValidator : AbstractValidator<CreateOpportunityCommand>
    {
        private static readonly AnswerType[] ChoiceAnswerTypes =
            new[] { AnswerType.SingleChoice, AnswerType.MultipleChoice };

        public CreateOpportunityValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync((id, ct) => unitOfWork.Organizations.ExistsAsync(id, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound])
                .MustAsync((id, ct) => unitOfWork.Organizations.IsVerifiedAsync(id, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.OrganizationMustBeVerified]);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MaximumLength(100).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 100));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MaximumLength(5000).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 5000));

            RuleFor(x => x.PhotoKey)
                .Must(photoKey => string.IsNullOrWhiteSpace(photoKey) || photoKey.Trim().Length <= 500)
                .WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 500));

            RuleFor(x => x.WorkType)
                .IsInEnum().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.LocationType)
                .IsInEnum().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .GreaterThan(x => x.StartDate)
                .WithMessage(localizer[SharedResourcesKeys.Validation.EndDateAfterStartDate]);

            RuleFor(x => x.NumberOfVolunteers)
                .GreaterThan(0).WithMessage(localizer[SharedResourcesKeys.Validation.OutOfRange]);

            RuleFor(x => x.Addresses)
                .NotNull().NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .Must(addresses => addresses.Select(a => a.CityId).Distinct().Count() == addresses.Count)
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry])
                .MustAsync(async (addresses, ct) =>
                {
                    var cityGovernoratePairs = addresses.ToDictionary(a => a.CityId, a => a.GovernorateId);
                    return await unitOfWork.Cities.AreValidCityGovernoratePairsAsync(
                        cityGovernoratePairs.Keys.ToList(),
                        cityGovernoratePairs,
                        ct);
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleForEach(x => x.Addresses).ChildRules(address =>
            {
                address.RuleFor(x => x.GovernorateId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

                address.RuleFor(x => x.CityId)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

                address.RuleFor(x => x.Description)
                    .MaximumLength(500).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 500));
            });

            RuleFor(x => x.ProvidedSkillIds)
                .Must(skillIds => skillIds is null || (skillIds.All(id => id != Guid.Empty) && skillIds.Distinct().Count() == skillIds.Count))
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleFor(x => x.RequiredSkillIds)
                .Must(skillIds => skillIds is null || (skillIds.All(id => id != Guid.Empty) && skillIds.Distinct().Count() == skillIds.Count))
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleFor(x => x.FieldIds)
                .Must(fieldIds => fieldIds is null || (fieldIds.All(id => id != Guid.Empty) && fieldIds.Distinct().Count() == fieldIds.Count))
                .WithMessage(localizer[SharedResourcesKeys.Validation.DuplicateEntry]);

            RuleFor(x => x.FieldIds)
                .MustAsync(async (fieldIds, ct) =>
                {
                    if (fieldIds is null || fieldIds.Count == 0)
                        return true;

                    return await unitOfWork.Fields.AreAllExistingAsync(fieldIds.Distinct().ToList(), ct);
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.RequiredSkillIds)
                .Must((command, requiredSkillIds) =>
                {
                    var providedSkillIds = command.ProvidedSkillIds ?? new List<Guid>();
                    var required = requiredSkillIds ?? new List<Guid>();

                    return !providedSkillIds.Intersect(required).Any();
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.SkillCannotBeBothRequiredAndProvided]);

            RuleFor(x => x.ProvidedSkillIds)
                .MustAsync(async (command, providedSkillIds, ct) =>
                {
                    var provided = providedSkillIds ?? new List<Guid>();
                    var required = command.RequiredSkillIds ?? new List<Guid>();

                    var allSkillIds = provided
                        .Concat(required)
                        .Distinct()
                        .ToList();

                    if (allSkillIds.Count == 0)
                        return true;

                    return await unitOfWork.Skills.AreAllExistingAsync(allSkillIds, ct);
                })
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);


            RuleForEach(x => x.Questions!).ChildRules(question =>
            {
                question.RuleFor(q => q.Description)
                    .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                    .MaximumLength(1000).WithMessage(string.Format(localizer[SharedResourcesKeys.Validation.MaxLength], 1000));

                question.RuleFor(q => q.AnswerType)
                    .IsInEnum().WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

                question.RuleFor(q => q.Options)
                    .Must((q, options) =>
                    {
                        if (!ChoiceAnswerTypes.Contains(q.AnswerType))
                            return options is null || options.Count == 0;

                        if (options is null || options.Count < 2)
                            return false;

                        var normalizedOptions = options
                            .Where(opt => !string.IsNullOrWhiteSpace(opt))
                            .Select(opt => opt.Trim())
                            .ToList();

                        return normalizedOptions.Count == options.Count
                               && normalizedOptions.Distinct(StringComparer.OrdinalIgnoreCase).Count() == options.Count;
                    })
                    .WithMessage(localizer[SharedResourcesKeys.Validation.InvalidQuestionOptions]);
            })
            .When(x => x.Questions is not null);
        }
    }
}
