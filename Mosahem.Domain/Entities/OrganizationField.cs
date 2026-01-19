using mosahem.Domain.Entities.MasterData;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Domain.Entities
{
    public class OrganizationField
    {
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public Guid FieldId { get; set; }
        public Field Field { get; set; }    
    }
}
