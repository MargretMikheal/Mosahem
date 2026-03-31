using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Queries.GetAllOrganizations
{
    public class GetAllOrganizationsQuery : IRequest<Response<List<GetAllOrganizationsResponse>>>
    {
    }
}
