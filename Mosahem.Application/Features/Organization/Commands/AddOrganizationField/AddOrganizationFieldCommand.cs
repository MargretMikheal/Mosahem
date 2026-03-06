using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Commands.AddOrganizationField
{
    public class AddOrganizationFieldCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid FieldId { get; set; }
    }
}
