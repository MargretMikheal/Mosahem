using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.SaveOpportunity
{
    public class SaveOpportunityCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OpportunityId { get; set; }
    }
}
