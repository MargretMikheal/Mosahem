using Mapster;
using mosahem.Domain.Entities.Questions;
using Mosahem.Application.Features.Opportunities.Queries.GetQuestionsAnswers;
using System.Text.Json;

namespace Mosahem.Application.Mapping
{
    public class QuestionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<QuestionAnswer, GetQuestionsAnswerResponse>()
                .Map(dest => dest.QuestionId, src => src.QuestionId)
                .Map(dest => dest.Order, src => src.Question.Order)
                .Map(dest => dest.Description, src => src.Question.Description)
                .Map(dest => dest.AnswerType, src => src.Question.AnswerType.ToString())
                .Map(dest => dest.Options, src => ParseQuestionOptions(src.Question.Options))
                .Map(dest => dest.IsRequired, src => src.Question.IsRequired)

                .Map(dest => dest.AnswerText, src => src.AnswerText)
                .Map(dest => dest.ChoiceKey, src => src.ChoiceKey)
                .Map(dest => dest.SelectedChoices, src => ParseQuestionOptions(src.Json));
        }
        private static List<string> ParseQuestionOptions(JsonDocument? options)
        {
            if (options is null || options.RootElement.ValueKind != JsonValueKind.Array)
            {
                return new List<string>();
            }

            return options.RootElement
                .EnumerateArray()
                .Where(option => option.ValueKind == JsonValueKind.String)
                .Select(option => option.GetString())
                .Where(option => !string.IsNullOrWhiteSpace(option))
                .Select(option => option!)
                .ToList();
        }
    }
}
