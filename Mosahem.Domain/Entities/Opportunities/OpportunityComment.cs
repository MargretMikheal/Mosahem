using mosahem.Domain.Entities.Profiles;
using Mosahem.Domain.Entities;

namespace mosahem.Domain.Entities.Opportunities
{
    public class OpportunityComment : BaseEntity
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public string Text { get; set; }
        public bool IsHidden { get; set; }

    }
}
