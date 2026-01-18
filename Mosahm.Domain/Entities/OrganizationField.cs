using Mosahm.Domain.Entities.MasterData;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Domain.Entities
{
    public class OrganizationField
    {
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public Guid FieldId { get; set; }
        public Field Field { get; set; }    
    }
}
