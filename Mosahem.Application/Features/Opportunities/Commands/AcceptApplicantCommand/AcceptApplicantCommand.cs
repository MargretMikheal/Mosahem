using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.AcceptApplicantCommand
{
    public class AcceptApplicantCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid OpportunityId { get; set; }
        public Guid ApplicantId { get; set; }

        public AcceptApplicantCommand(Guid organizationId, Guid opportunityId, Guid applicantId)
        {
            OrganizationId = organizationId;
            OpportunityId = opportunityId;
            ApplicantId = applicantId;
        }
    }
}
