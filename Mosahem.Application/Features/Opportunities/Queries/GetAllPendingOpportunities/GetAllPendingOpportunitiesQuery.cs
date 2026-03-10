using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllPendingOpportunities
{
    public class GetAllPendingOpportunitiesQuery : IRequest<Response<List<PendingOpportunityResponse>>> { }
}
