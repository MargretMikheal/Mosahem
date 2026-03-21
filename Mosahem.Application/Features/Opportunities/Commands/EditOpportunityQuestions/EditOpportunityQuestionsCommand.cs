using MediatR;
using mosahem.Application.Common;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityQuestions
{
    public class EditOpportunityQuestionsCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
        public Guid OrganizationId { get; set; }
        public List<EditOpportunityQuestionDto> Questions { get; set; }

        public EditOpportunityQuestionsCommand(Guid opportunityId, Guid organizationId, List<EditOpportunityQuestionDto> questions)
        {
            OpportunityId = opportunityId;
            OrganizationId = organizationId;
            Questions = questions;
        }
    }
    public class EditOpportunityQuestionDto
    {
        public Guid? QuestionId { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; }
    }
}
