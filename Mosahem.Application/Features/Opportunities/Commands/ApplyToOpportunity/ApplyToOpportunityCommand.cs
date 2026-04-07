using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.ApplyToOpportunity
{
    public class ApplyToOpportunityCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OpportunityId { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; }
    }

    public class QuestionAnswerDto
    {
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? ChoiceKey { get; set; }
        public List<string>? SelectedChoices { get; set; }
    }
}
