using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Commands.DeleteOrganizationField
{
    public class DeleteOrganizationFieldCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid FieldId { get; set; }
    }
}
