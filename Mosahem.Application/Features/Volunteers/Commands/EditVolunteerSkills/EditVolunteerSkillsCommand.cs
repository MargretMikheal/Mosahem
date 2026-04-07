using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerSkills
{
    public class EditVolunteerSkillsCommand : IRequest<Response<string>>
    {
        public EditVolunteerSkillsCommand(Guid volunteerId, HashSet<Guid> skillIds)
        {
            VolunteerId = volunteerId;
            SkillIds = skillIds;
        }

        public Guid VolunteerId { get; set; }
        public HashSet<Guid> SkillIds { get; set; }
    }
}
