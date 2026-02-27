using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Admin.Commands.EditBasicInfo
{
    public class EditAdminInfoCommand : IRequest<Response<string>>
    {
        public Guid AdminId { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
