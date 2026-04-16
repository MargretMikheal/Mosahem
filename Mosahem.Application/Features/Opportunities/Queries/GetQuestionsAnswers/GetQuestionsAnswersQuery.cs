using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetQuestionsAnswers
{
    public class GetQuestionAnswersQuery : IRequest<Response<IReadOnlyList<GetQuestionsAnswerResponse>>>
    {
        public Guid? OrganizationId { get; set; }
        public Guid VolunteerId { get; set; }
        public Guid OpportunityId { get; set; }
    }
}
