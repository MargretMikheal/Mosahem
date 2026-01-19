using mosahem.Domain.Entities.MasterData;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Domain.Entities
{
    public class VolunteerField
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid FieldId { get; set; }
        public Field Field {  get; set; }
    }
}
