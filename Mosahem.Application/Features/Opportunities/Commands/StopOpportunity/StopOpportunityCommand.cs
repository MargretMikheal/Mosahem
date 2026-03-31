using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.StopOpportunity
{
    public class StopOpportunityCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
    }
}
