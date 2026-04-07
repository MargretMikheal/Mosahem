using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Questions;
using mosahem.Domain.Enums;
using System.Text.Json;

namespace Mosahem.Application.Features.Opportunities.Commands.ApplyToOpportunity
{
    public class ApplyToOpportunityCommandValidator : AbstractValidator<ApplyToOpportunityCommand>
    {
        public ApplyToOpportunityCommandValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.VolunteerId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.OpportunityId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Opportunities.GetByIdAsync(id, ct) is not null)
                .WithMessage(localizer[SharedResourcesKeys.Validation.NotFound]);

            RuleFor(x => x.Answers)
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleForEach(x => x.Answers)
                .SetValidator(new QuestionAnswerDtoValidator(localizer, unitOfWork));
        }
    }

    public class QuestionAnswerDtoValidator : AbstractValidator<QuestionAnswerDto>
    {
        public QuestionAnswerDtoValidator(
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required])
                .MustAsync(async (id, ct) => await unitOfWork.Repository<Question>()
                    .GetTableNoTracking()
                    .AnyAsync(q => q.Id == id, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);

            RuleFor(x => x)
                .Must(answer =>
                {
                    var filledCount = 0;
                    if (!string.IsNullOrWhiteSpace(answer.AnswerText)) filledCount++;
                    if (answer.ChoiceKey is not null) filledCount++;
                    if (answer.SelectedChoices is not null && answer.SelectedChoices.Any()) filledCount++;
                    return filledCount <= 1;
                }).WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .MustAsync(async (answer, ct) => await ValidateAnswerByTypeAsync(answer, unitOfWork, ct))
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid])
                .When(x => x.QuestionId != Guid.Empty);
        }

        private static async Task<bool> ValidateAnswerByTypeAsync(
            QuestionAnswerDto answer,
            IUnitOfWork unitOfWork,
            CancellationToken cancellationToken)
        {
            var question = await unitOfWork.Repository<Question>()
                  .GetTableNoTracking()
                  .FirstOrDefaultAsync(q => q.Id == answer.QuestionId, cancellationToken);

            if (question is null)
                return false;

            if (!question.IsRequired && !HasAnyAnswer(answer))
                return true;

            return question.AnswerType switch
            {
                AnswerType.Text => ValidateText(answer.AnswerText),
                AnswerType.TextArea => ValidateTextArea(answer.AnswerText),
                AnswerType.SingleChoice => ValidateSingleChoice(answer.ChoiceKey, question.Options),
                AnswerType.MultipleChoice => ValidateMultipleChoice(answer.SelectedChoices, question.Options),
                AnswerType.Bool => ValidateBool(answer.AnswerText),
                AnswerType.Int => ValidateInt(answer.AnswerText),
                _ => false
            };
        }

        private static bool HasAnyAnswer(QuestionAnswerDto answer)
            => !string.IsNullOrWhiteSpace(answer.AnswerText)
            || answer.ChoiceKey is not null
            || answer.SelectedChoices is not null && answer.SelectedChoices.Any();

        private static bool ValidateText(string? text)
            => !string.IsNullOrWhiteSpace(text) && text.Length <= 500;

        private static bool ValidateTextArea(string? text)
            => !string.IsNullOrWhiteSpace(text) && text.Length <= 2000;

        private static bool ValidateSingleChoice(int? choiceKey, JsonDocument? options)
        {
            if (choiceKey is null || options is null)
                return false;

            var count = options.RootElement.GetArrayLength();
            return choiceKey >= 0 && choiceKey < count;
        }

        private static bool ValidateMultipleChoice(List<string>? selectedChoices, JsonDocument? options)
        {
            if (selectedChoices is null || !selectedChoices.Any() || options is null)
                return false;

            var validChoices = options.RootElement
                .EnumerateArray()
                .Select(e => e.GetString())
                .ToHashSet();

            return selectedChoices.All(choice => validChoices.Contains(choice));
        }

        private static bool ValidateBool(string? text)
            => !string.IsNullOrEmpty(text)
            && text.ToLowerInvariant() is "true" or "false";

        private static bool ValidateInt(string? text)
            => !string.IsNullOrWhiteSpace(text) && int.TryParse(text, out _);
    }
}
