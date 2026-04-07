namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerSkills
{
    public class EditVolunteerSkillsRequest
    {
        public HashSet<Guid> SkillsIds { get; set; }

        public EditVolunteerSkillsRequest(HashSet<Guid> skillsIds)
        {
            SkillsIds = skillsIds;
        }
    }
}
