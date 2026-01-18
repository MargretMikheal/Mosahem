using Mosahm.Domain.Entities.MasterData;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Domain.Entities
{
    public class VolunteerSkill
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
