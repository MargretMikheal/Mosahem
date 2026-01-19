using mosahem.Domain.Entities.Profiles;

namespace mosahem.Domain.Entities
{
    public class OrganizationFollower
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
