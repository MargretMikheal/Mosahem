using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationFields
{
    public class GetOrganizationFieldsQuery : IRequest<Response<List<GetOrganizationFieldsResponse>>>
    {
        public Guid OrganizationId { get; set; }
    }
}
