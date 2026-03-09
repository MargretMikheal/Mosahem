using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllAcceptedOpportunities
{
    public class GetAllAcceptedOpportunitiesQuery : IRequest<Response<List<AcceptedOpportunityResponse>>> { }
}
