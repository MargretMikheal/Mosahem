using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery : IRequest<Response<IReadOnlyList<GetAllAdminsQueryResponse>>>
    {
    }
}
