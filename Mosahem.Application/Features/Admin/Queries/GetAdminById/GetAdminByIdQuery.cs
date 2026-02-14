using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Admin.Queries.GetAdminById
{
    public class GetAdminByIdQuery : IRequest<Response<GetAdminByIdQueryResponse>>
    {
        public Guid AdminId { get; set; }

        public GetAdminByIdQuery(Guid adminId)
        {
            AdminId = adminId;
        }
    }
}
