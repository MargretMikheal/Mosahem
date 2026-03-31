using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DeleteSkillCommand(Guid id)
        {
            Id = id;
        }
    }
}
