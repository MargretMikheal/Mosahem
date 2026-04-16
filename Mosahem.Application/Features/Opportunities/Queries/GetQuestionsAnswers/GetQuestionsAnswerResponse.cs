namespace Mosahem.Application.Features.Opportunities.Queries.GetQuestionsAnswers
{
    public class GetQuestionsAnswerResponse
    {
        public Guid QuestionId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string AnswerType { get; set; }
        public List<string>? Options { get; set; }
        public bool IsRequired { get; set; }

        public string? AnswerText { get; set; }
        public int? ChoiceKey { get; set; }
        public List<string>? SelectedChoices { get; set; }
    }
}
