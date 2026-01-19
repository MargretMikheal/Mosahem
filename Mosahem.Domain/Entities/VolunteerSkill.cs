using mosahem.Domain.Entities.MasterData;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Domain.Entities
{
    public class VolunteerSkill
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
