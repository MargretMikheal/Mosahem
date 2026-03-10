using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllRejectedOpportunities
{
    public class GetAllRejectedOpportunitiesQuery : IRequest<Response<List<RejectedOpportunityResponse>>> { }
}
