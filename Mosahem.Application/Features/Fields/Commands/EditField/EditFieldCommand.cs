using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Fields.Commands.EditField
{
    public class EditFieldCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
