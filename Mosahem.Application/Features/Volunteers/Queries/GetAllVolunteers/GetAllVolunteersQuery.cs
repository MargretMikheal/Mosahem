using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Queries.GetAllVolunteers
{
    public class GetAllVolunteersQuery : IRequest<Response<IReadOnlyList<GetAllVolunteersQueryResponse>>>
    {
    }
}
