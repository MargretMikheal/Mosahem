using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityLikes
{
    public class GetOpportunityLikesQuery : IRequest<Response<List<GetOpportunityLikesResponse>>>
    {
        public Guid OpportunityId { get; set; }
    }
}
