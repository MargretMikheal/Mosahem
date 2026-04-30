using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.CommentOpportunity
{
    public class CommentOpportunityCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OpportunityId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
