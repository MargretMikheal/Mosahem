namespace Mosahm.Domain.Entities
{
    public class OpportunitySave
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid OpportunityId {  get; set; }
        public Opportunity Opportunity { get; set; }
    }
}
