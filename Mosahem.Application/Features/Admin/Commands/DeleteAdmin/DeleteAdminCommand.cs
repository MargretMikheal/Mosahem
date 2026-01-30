using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest<Response<string>>
    {
        public Guid AdminId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}