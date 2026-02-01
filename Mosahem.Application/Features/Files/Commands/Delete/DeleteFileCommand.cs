using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Files.Commands.Delete
{
    public class DeleteFileCommand : IRequest<Response<string>>
    {
        public string FileKey { get; set; }
    }
}
