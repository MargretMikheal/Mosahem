namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityQuestions
{
    public class EditOpportunityQuestionsRequest
    {
        public Guid OpportunityId { get; set; }
        public List<EditOpportunityQuestionDto> Questions { get; set; }
    }
}
