using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunitySaves
{
    public class GetOpportunitySavesQuery : IRequest<Response<List<GetOpportunitySavesResponse>>>
    {
        public Guid OpportunityId { get; set; }
    }
}
