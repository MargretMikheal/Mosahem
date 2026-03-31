using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.ResumeOpportunity
{
    public class ResumeOpportunityCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
    }
}
