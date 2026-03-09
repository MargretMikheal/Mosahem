using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.RejectOpportunity
{
    public class RejectOpportunityCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
    }
}
