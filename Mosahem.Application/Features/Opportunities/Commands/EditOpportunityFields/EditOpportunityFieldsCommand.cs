using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityFields
{
    public class EditOpportunityFieldsCommand : IRequest<Response<string>>
    {
        public EditOpportunityFieldsCommand(Guid opportunityId, HashSet<Guid> fieldIds, Guid organizationId)
        {
            OpportunityId = opportunityId;
            FieldIds = fieldIds;
            OrganizationId = organizationId;
        }

        public Guid OpportunityId { get; set; }
        public Guid OrganizationId { get; set; }
        public HashSet<Guid> FieldIds { get; set; }
    }
}
