using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Queries.GetAllOrganizations
{
    public class GetAllOrganizationsQuery : IRequest<Response<List<GetAllOrganizationsResponse>>>
    {
    }
}
