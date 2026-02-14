using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Skills.Commands.EditSkill
{
    public class EditSkillCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Category { get; set; }
    }
}
