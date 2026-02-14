using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommand : IRequest<Response<string>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Category { get; set; }
    }
}
