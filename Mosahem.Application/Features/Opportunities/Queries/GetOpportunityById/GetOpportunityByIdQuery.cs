using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById
{
    public class GetOpportunityByIdQuery : IRequest<Response<OpportunityDetailsResponse>>
    {
        public Guid OpportunityId { get; set; }
    }
}
