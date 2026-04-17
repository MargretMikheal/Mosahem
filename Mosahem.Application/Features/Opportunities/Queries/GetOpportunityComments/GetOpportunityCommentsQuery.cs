using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityComments
{
    public class GetOpportunityCommentsQuery : IRequest<Response<List<GetOpportunityCommentsResponse>>>
    {
        public Guid OpportunityId { get; set; }
    }
}
