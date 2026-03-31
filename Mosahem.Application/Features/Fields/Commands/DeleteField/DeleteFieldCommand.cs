using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Fields.Commands.DeleteField
{
    public class DeleteFieldCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public DeleteFieldCommand(Guid id)
        {
            Id = id;
        }
    }
}
