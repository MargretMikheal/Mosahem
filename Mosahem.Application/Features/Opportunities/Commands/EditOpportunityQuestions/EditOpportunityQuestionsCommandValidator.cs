using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityQuestions
{
    public class EditOpportunityQuestionsCommandValidator : AbstractValidator<EditOpportunityQuestionsCommand>
    {
        private static readonly AnswerType[] ChoiceAnswerTypes = { AnswerType.SingleChoice, AnswerType.MultipleChoice };
        public EditOpportunityQuestionsCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);


            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleForEach(x => x.Questions)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .ChildRules(question =>
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
            });
        }
    }
}
