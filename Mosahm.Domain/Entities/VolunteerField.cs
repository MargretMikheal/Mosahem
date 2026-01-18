using Mosahm.Domain.Entities.MasterData;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Domain.Entities
{
    public class VolunteerField
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid FieldId { get; set; }
        public Field Field {  get; set; }
    }
}
