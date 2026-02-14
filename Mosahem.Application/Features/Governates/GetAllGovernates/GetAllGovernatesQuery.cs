using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Governates.GetAllGovernates
{
    public class GetAllGovernatesQuery : IRequest<Response<IReadOnlyList<GetAllGovernatesQueryResponse>>>
    {
    }
}
