using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Fields.Queries.GetAllFields
{
    public class GetAllFieldsQuery : IRequest<Response<IReadOnlyList<GetAllFieldsQueryResponse>>>
    {
    }
}
