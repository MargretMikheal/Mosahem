using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.LikeOpportunity
{
    public class LikeOpportunityCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OpportunityId { get; set; }
    }
}
