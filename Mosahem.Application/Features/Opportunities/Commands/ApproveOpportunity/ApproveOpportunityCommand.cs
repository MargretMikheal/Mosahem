using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.ApproveOpportunity
{
    public class ApproveOpportunityCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
    }
}
