using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationFields
{
    public class EditOrganizationFieldsCommand : IRequest<Response<string>>
    {
        public EditOrganizationFieldsCommand(Guid organizationId, HashSet<Guid> fieldsIds)
        {
            OrganizationId = organizationId;
            FieldsIds = fieldsIds;
        }

        public Guid OrganizationId { get; set; }
        public HashSet<Guid> FieldsIds { get; set; }
    }
}
