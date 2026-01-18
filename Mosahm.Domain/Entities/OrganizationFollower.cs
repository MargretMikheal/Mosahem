using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Domain.Entities
{
    public class OrganizationFollower
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
