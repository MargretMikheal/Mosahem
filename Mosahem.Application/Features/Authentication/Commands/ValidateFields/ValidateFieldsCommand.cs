using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateFields
{
    public class ValidateFieldsCommand : IRequest<Response<string>>
    {
        public List<Guid> FieldIds { get; set; }
    }
}
