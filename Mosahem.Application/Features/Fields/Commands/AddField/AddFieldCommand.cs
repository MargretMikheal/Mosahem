using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Fields.Commands.AddField
{
    public class AddFieldCommand : IRequest<Response<string>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
