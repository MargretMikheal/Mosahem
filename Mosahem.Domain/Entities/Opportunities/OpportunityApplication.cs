using mosahem.Domain.Common.Localization;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;

namespace mosahem.Domain.Entities.Opportunities
{
    public class OpportunityApplication : BaseEntity
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public ApplicantStatus ApplicantStatus { get; set; }
    }
}
