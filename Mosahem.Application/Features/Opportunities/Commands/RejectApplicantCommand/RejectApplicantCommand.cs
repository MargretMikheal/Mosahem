using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.RejectApplicantCommand
{
    public class RejectApplicantCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid OpportunityId { get; set; }
        public Guid ApplicantId { get; set; }

        public RejectApplicantCommand(Guid organizationId, Guid opportunityId, Guid applicantId)
        {
            OrganizationId = organizationId;
            OpportunityId = opportunityId;
            ApplicantId = applicantId;
        }
    }
}
