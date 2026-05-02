using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.RateApplicant
{
    public class RateApplicantCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid OpportunityId { get; set; }
        public Guid VolunteerId { get; set; }
        public int Rate { get; set; }
    }
}
