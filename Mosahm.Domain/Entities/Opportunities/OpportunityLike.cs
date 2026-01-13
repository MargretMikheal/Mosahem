using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Domain.Entities.Opportunities
{
    public class OpportunityLike
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
    }
}
